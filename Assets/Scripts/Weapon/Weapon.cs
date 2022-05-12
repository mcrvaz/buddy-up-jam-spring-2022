using System;
using System.Collections;
using UnityEngine;

public class Weapon : IWeapon
{
    public event Action OnAmmoUpdated;
    public event Action<float> OnReloadStart;
    public event Action OnReloadEnd;
    public event Action OnShoot;
    public event Action OnEmptyAmmoFire;

    public event Action<float> OnSwapInStart;
    public event Action<float> OnSwapOutStart;

    public event Action<IWeapon> OnSwapOutEnded;
    public event Action<IWeapon> OnSwapInEnded;

    public WeaponId WeaponId => settings.WeaponId;
    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => settings.MaxAmmo;
    public int RoundsInClip { get; private set; }
    public int RoundsPerClip => settings.RoundsPerClip;
    public int Damage => settings.Damage;
    public float SwapInTime => settings.SwapInTime;
    public float SwapOutTime => settings.SwapOutTime;
    public bool CanShoot
    {
        get
        {
            if (isReloading)
                return false;
            var time = Time.time;
            if (time < nextShotTime)
                return false;
            if (time < swapOutEndTime)
                return false;
            if (time < swapInEndTime)
                return false;
            return true;
        }
    }

    protected readonly ProjectilePool pool;
    protected readonly Transform weaponTransform;
    protected readonly Transform[] projectileSpawnPoints;
    protected readonly WeaponSettings settings;
    protected readonly MonoBehaviour coroutineRunner;
    protected readonly Camera camera;
    protected readonly CameraShake cameraShake;

    float nextShotTime = float.MinValue;
    float swapInEndTime = float.MinValue;
    float swapOutEndTime = float.MinValue;
    float reloadEndTime;
    bool isReloading;

    Coroutine weaponSwapRoutine;
    Coroutine reloadRoutine;

    public Weapon (
        Transform weaponTransform,
        Transform[] projectileSpawnPoints,
        WeaponSettings settings,
        MonoBehaviour coroutineRunner,
        Camera camera,
        CameraShake cameraShake
    )
    {
        pool = new ProjectilePool(settings.ProjectilePrefab);
        this.projectileSpawnPoints = projectileSpawnPoints;
        this.settings = settings;
        this.weaponTransform = weaponTransform;
        this.coroutineRunner = coroutineRunner;
        this.camera = camera;
        this.cameraShake = cameraShake;
        SetupInitialAmmo();
    }

    public void SetAsActive ()
    {
        StopReloading();
        if (RoundsInClip <= 0)
            Reload();
    }

    public void SetAsInactive ()
    {
        StopReloading();
    }

    public void Update ()
    {
        RotateWeaponToScreenCenter();
    }

    public void Shoot ()
    {
        if (!CanShoot)
            return;

        for (int i = 0; i < settings.ProjectileCount; i++)
        {
            if (!ConsumeAmmo())
            {
                OnEmptyAmmoFire?.Invoke();
                return;
            }
            ShootProjectile(projectileSpawnPoints[i]);
        }
        nextShotTime = Time.time + settings.IntervalBetweenShots;
        PlayCameraShake();
        OnShoot?.Invoke();

        if (RoundsInClip <= 0)
            Reload();
    }

    public void RestoreAmmo (int amount)
    {
        CurrentAmmo = Mathf.Min(CurrentAmmo + amount, MaxAmmo);
        OnAmmoUpdated?.Invoke();

        if (RoundsInClip <= 0)
            Reload();
    }

    public void Reload ()
    {
        if (isReloading)
            return;
        if (RoundsInClip == RoundsPerClip)
            return;
        StartReloadRoutine();
    }

    public void SwapOut ()
    {
        StopReloading();

        if (weaponSwapRoutine != null)
            coroutineRunner.StopCoroutine(weaponSwapRoutine);
        weaponSwapRoutine = coroutineRunner.StartCoroutine(
            SwapOutRoutine()
        );
    }

    public void SwapIn ()
    {
        StopReloading();

        if (weaponSwapRoutine != null)
            coroutineRunner.StopCoroutine(weaponSwapRoutine);
        weaponSwapRoutine = coroutineRunner.StartCoroutine(
            SwapInRoutine()
        );
    }

    protected virtual void PlayCameraShake () { }

    protected virtual void ShootProjectile (Transform spawnPoint)
    {
        var projectile = pool.Get();
        projectile.transform.position = spawnPoint.position;
        projectile.Damage = Damage;
        projectile.Forward = weaponTransform.forward;
        EvaluateHitsBetweenCameraAndProjectileSpawnPoint(projectile, spawnPoint);
    }

    protected void EvaluateHitsBetweenCameraAndProjectileSpawnPoint (ProjectileBehaviour projectile, Transform spawnPoint)
    {
        var spawnDistanceFromCamera = Vector3.Distance(spawnPoint.position, camera.transform.position) + 0.1f;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit, spawnDistanceFromCamera))
        {
            // TODO maybe teleport projectile to collision point
            if (hit.collider.TryGetComponent<EnemyBodyPartBehaviour>(out var bodyPart))
                bodyPart.ForceCollision(projectile.Collider);
            projectile.Projectile.HandleCollision(hit.collider);
        }
    }

    void StartReloadRoutine ()
    {
        reloadRoutine = coroutineRunner.StartCoroutine(ReloadRoutine());
    }

    void StopReloading ()
    {
        isReloading = false;
        if (reloadRoutine != null)
            coroutineRunner.StopCoroutine(reloadRoutine);
    }

    bool ConsumeAmmo ()
    {
        if (RoundsInClip <= 0)
            return false;

        RoundsInClip--;
        OnAmmoUpdated?.Invoke();
        return true;
    }

    void ReloadClip ()
    {
        int diff = RoundsPerClip - RoundsInClip;
        RoundsInClip = Mathf.Min(CurrentAmmo, RoundsPerClip);
        CurrentAmmo = Mathf.Max(0, CurrentAmmo - diff);
        OnAmmoUpdated?.Invoke();
        OnReloadEnd?.Invoke();
    }

    void SetupInitialAmmo ()
    {
        CurrentAmmo = settings.StartingAmmo;
        ReloadClip();
        OnAmmoUpdated?.Invoke();
    }

    IEnumerator SwapOutRoutine ()
    {
        float swapOutDuration = SwapOutTime;
        swapOutEndTime = Time.time + swapOutDuration;
        OnSwapOutStart?.Invoke(SwapOutTime);
        while (Time.time < swapOutEndTime)
            yield return null;
        OnSwapOutEnded?.Invoke(this);
    }

    IEnumerator SwapInRoutine ()
    {
        float swapInDuration = SwapInTime;
        swapInEndTime = Time.time + swapInDuration;
        OnSwapInStart?.Invoke(SwapInTime);
        while (Time.time < swapInEndTime)
            yield return null;
        OnSwapInEnded?.Invoke(this);
    }

    IEnumerator ReloadRoutine ()
    {
        while (Time.time < swapInEndTime)
            yield return null;
        while (Time.time < nextShotTime)
            yield return null;

        OnReloadStart?.Invoke(settings.ReloadTime);
        isReloading = true;
        reloadEndTime = Time.time + settings.ReloadTime;
        while (Time.time < reloadEndTime)
            yield return null;
        isReloading = false;
        ReloadClip();
    }

    void RotateWeaponToScreenCenter ()
    {
        var screenCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000f));
        weaponTransform.LookAt(screenCenter);
    }
}