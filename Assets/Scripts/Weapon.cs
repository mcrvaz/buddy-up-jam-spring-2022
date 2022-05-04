using UnityEngine;

public partial class Weapon
{
    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => settings.MaxAmmo;
    public int CurrentRounds { get; private set; }
    public int MaxRounds => settings.MaxRounds;

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
    }

    public void Shoot ()
    {
        for (int i = 0; i < settings.ProjectileCount; i++)
        {
            var projectile = pool.Get();
            projectile.transform.position = projectileSpawnPoints[i].position;
            projectile.Forward = weaponTransform.forward;
        }
    }
}