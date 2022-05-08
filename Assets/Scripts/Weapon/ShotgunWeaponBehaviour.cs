using UnityEngine;

public class ShotgunWeaponBehaviour : WeaponBehaviour<ShotgunWeapon>
{
    [field: SerializeField] public ShotgunWeaponSettings WeaponSettings { get; private set; }

    public ShotgunWeapon ShotgunWeapon => (ShotgunWeapon)Weapon;

    protected override ShotgunWeapon CreateWeapon ()
    {
        return new ShotgunWeapon(
            WeaponTransform,
            ProjectileSpawnPoints,
            WeaponSettings,
            this,
            playerBehaviour.WeaponCamera,
            playerBehaviour.MainCamera.CameraShake
        );
    }
}