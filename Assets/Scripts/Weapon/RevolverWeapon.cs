using UnityEngine;

public class RevolverWeapon : Weapon
{
    public RevolverWeapon (
        Transform weaponTransform,
        Transform[] projectileSpawnPoints,
        WeaponSettings settings,
        MonoBehaviour coroutineRunner,
        Camera camera,
        CameraShake cameraShake
    ) : base(weaponTransform, projectileSpawnPoints, settings, coroutineRunner, camera, cameraShake) { }

    protected override void PlayCameraShake ()
    {
        cameraShake.PlayLightShake();
    }
}