using UnityEngine;

public class ShotgunWeapon : Weapon
{
    const float BARREL_RADIUS = 0.1f;

    readonly ShotgunWeaponSettings shotgunWeaponSettings;

    public ShotgunWeapon (
        Transform weaponTransform,
        Transform[] projectileSpawnPoints,
        ShotgunWeaponSettings settings,
        MonoBehaviour coroutineRunner,
        Camera camera,
        CameraShake cameraShake
    ) : base(weaponTransform, projectileSpawnPoints, settings, coroutineRunner, camera, cameraShake)
    {
        shotgunWeaponSettings = settings;
    }

    protected override void PlayCameraShake ()
    {
        cameraShake.PlayStrongShake();
    }

    protected override void ShootProjectile (Transform spawnPoint)
    {
        int pelletCount = shotgunWeaponSettings.PelletCount;
        float pelletDamage = shotgunWeaponSettings.Damage / (float)pelletCount;
        for (int i = 0; i < shotgunWeaponSettings.PelletCount; i++)
        {
            var projectile = pool.Get();
            float spawnPointPlane = spawnPoint.position.z;
            Vector2 random = Random.insideUnitCircle * BARREL_RADIUS;
            Vector3 projectileStartPoint = new Vector3(
                spawnPoint.position.x + random.x,
                spawnPoint.position.y + random.y,
                spawnPointPlane
            );

            projectile.transform.position = projectileStartPoint;
            projectile.Damage = pelletDamage;
            projectile.Forward = weaponTransform.forward;
            var spread = new Vector3(
                RandomUtils.Range(shotgunWeaponSettings.SpreadVerticalAngleRange),
                RandomUtils.Range(-shotgunWeaponSettings.SpreadHorizontalAngleRange),
                0
            );
            projectile.transform.Rotate(spread);
        }
    }
}