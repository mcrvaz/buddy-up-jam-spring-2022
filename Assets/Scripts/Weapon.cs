using System;
using UnityEngine;

public partial class Weapon
{
    public event Action OnAmmoUpdated;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => settings.MaxAmmo;
    public int RoundsInClip { get; private set; }
    public int RoundsPerClip => settings.RoundsPerClip;
    public int Damage => settings.Damage;

    readonly ProjectilePool pool;
    readonly Transform weaponTransform;
    readonly Transform[] projectileSpawnPoints;
    readonly WeaponSettings settings;

    public Weapon (Transform weaponTransform, Transform[] projectileSpawnPoints, WeaponSettings settings)
    {
        pool = new ProjectilePool(settings.ProjectilePrefab);
        this.projectileSpawnPoints = projectileSpawnPoints;
        this.settings = settings;
        this.weaponTransform = weaponTransform;
        SetupInitialAmmo();
    }

    public void Shoot ()
    {
        if (RoundsInClip <= 0)
        {
            ReloadClip();
            return;
        }

        for (int i = 0; i < settings.ProjectileCount; i++)
        {
            if (!ConsumeAmmo())
                return;
            var projectile = pool.Get();
            projectile.transform.position = projectileSpawnPoints[i].position;
            projectile.Forward = weaponTransform.forward;
        }
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
    }

    void SetupInitialAmmo ()
    {
        CurrentAmmo = settings.StartingAmmo;
        ReloadClip();
        OnAmmoUpdated?.Invoke();
    }
}