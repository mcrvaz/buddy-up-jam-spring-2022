using System;
using System.Collections;
using UnityEngine;

public class Weapon : IWeapon
{
    public event Action OnAmmoUpdated
    {
        add => weaponAmmo.OnAmmoUpdated += value;
        remove => weaponAmmo.OnAmmoUpdated -= value;
    }
    public event Action<float> OnReloadStart;
    public event Action OnReloadEnd;
    public event Action OnShoot;
    public event Action OnEmptyAmmoFire;

    public event Action<float> OnSwapInStart;
    public event Action<float> OnSwapOutStart;

    public event Action<IWeapon> OnSwapOutEnded;
    public event Action<IWeapon> OnSwapInEnded;

    public WeaponId WeaponId => settings.WeaponId;
    public int CurrentAmmo => weaponAmmo.CurrentAmmo;
    public int MaxAmmo => weaponAmmo.MaxAmmo;
    public int RoundsInClip => weaponAmmo.RoundsInClip;
    public int RoundsPerClip => weaponAmmo.RoundsPerClip;

    public int Damage => settings.Damage;
    public float SwapInTime => settings.SwapInTime;
    public float SwapOutTime => settings.SwapOutTime;
    public bool CanShoot
    {
        get
        {
            if (!enabled)
                return false;
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

    readonly WeaponAmmo weaponAmmo;

    float nextShotTime = float.MinValue;
    float swapInEndTime = float.MinValue;
    float swapOutEndTime = float.MinValue;
    float reloadEndTime;
    bool enabled;
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

        weaponAmmo = new WeaponAmmo(settings);
        weaponAmmo.SetupInitialAmmo();
    }

    public void Start ()
    {
        enabled = true;
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
            if (!weaponAmmo.ConsumeAmmo())
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
        weaponAmmo.RestoreAmmo(amount);
        if (RoundsInClip <= 0)
            Reload();
    }

    public void Reload ()
    {
        if (isReloading)
            return;
        if (RoundsInClip == RoundsPerClip)
            return;
        if (CurrentAmmo == 0)
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

    void ReloadClip ()
    {
        weaponAmmo.ReloadClip();
        OnReloadEnd?.Invoke();
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