using System;
using System.Collections;
using UnityEngine;

public class Weapon
{
    public event Action OnAmmoUpdated;
    public event Action<float> OnReloadStart;
    public event Action OnReloadEnd;
    public event Action OnShoot;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => settings.MaxAmmo;
    public int RoundsInClip { get; private set; }
    public int RoundsPerClip => settings.RoundsPerClip;
    public int Damage => settings.Damage;

    readonly ProjectilePool pool;
    readonly Transform weaponTransform;
    readonly Transform[] projectileSpawnPoints;
    readonly WeaponSettings settings;
    readonly MonoBehaviour coroutineRunner;
    readonly Camera camera;

    float nextShotTime = float.MinValue;
    float reloadEndTime;
    bool isReloading;

    public Weapon (
        Transform weaponTransform,
        Transform[] projectileSpawnPoints,
        WeaponSettings settings,
        MonoBehaviour coroutineRunner,
        Camera camera
    )
    {
        pool = new ProjectilePool(settings.ProjectilePrefab);
        this.projectileSpawnPoints = projectileSpawnPoints;
        this.settings = settings;
        this.weaponTransform = weaponTransform;
        this.coroutineRunner = coroutineRunner;
        SetupInitialAmmo();
        this.camera = camera;
    }

    public void Update ()
    {
        RotateWeaponToScreenCenter();
    }

    public void Shoot ()
    {
        if (isReloading)
            return;
        if (Time.time < nextShotTime)
            return;

        for (int i = 0; i < settings.ProjectileCount; i++)
        {
            if (!ConsumeAmmo())
                return;
            var projectile = pool.Get();
            projectile.transform.position = projectileSpawnPoints[i].position;
            projectile.Damage = Damage;
            projectile.Forward = weaponTransform.forward;
        }
        nextShotTime = Time.time + settings.IntervalBetweenShots;
        OnShoot?.Invoke();

        if (RoundsInClip <= 0)
            StartReloadRoutine();
    }

    void StartReloadRoutine ()
    {
        coroutineRunner.StartCoroutine(ReloadRoutine(true));
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
        RoundsInClip = Mathf.Min(CurrentAmmo, RoundsPerClip);
        CurrentAmmo = Mathf.Max(0, CurrentAmmo - RoundsInClip);
        OnAmmoUpdated?.Invoke();
        OnReloadEnd?.Invoke();
    }

    void SetupInitialAmmo ()
    {
        CurrentAmmo = settings.StartingAmmo;
        ReloadClip();
        OnAmmoUpdated?.Invoke();
    }

    IEnumerator ReloadRoutine (bool addShootingDelay)
    {
        if (addShootingDelay)
        {
            while (Time.time < nextShotTime)
                yield return null;
        }

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
        // var screenCenter = camera.ScreenToWorldPoint(
        //     new Vector3(Screen.width / 2f, Screen.height / 2f, 1000f)
        // );
        var screenCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000f));
        // Vector3 direction = screenCenter - weaponTransform.position;
        weaponTransform.LookAt(screenCenter);
        // weaponTransform.rotation = Quaternion.LookRotation(direction);
    }
}