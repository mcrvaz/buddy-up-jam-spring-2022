using UnityEngine;

public class RevolverWeaponBehaviour : WeaponBehaviour<RevolverWeapon>
{
    [field: SerializeField] public WeaponSettings WeaponSettings { get; private set; }

    public RevolverWeapon RevolverWeapon => (RevolverWeapon)Weapon;

    protected override RevolverWeapon CreateWeapon ()
    {
        return new RevolverWeapon(
            WeaponTransform,
            ProjectileSpawnPoints,
            WeaponSettings,
            FindObjectOfType<CoroutineRunner>(),
            playerBehaviour.WeaponCamera,
            playerBehaviour.MainCamera.CameraShake
        );
    }

    protected override IWeaponSounds CreateWeaponSounds ()
    {
        return new RevolverWeaponSounds(
            audioBehaviour.AudioManager,
            playerBehaviour.PlayerAudioSource
        );
    }
}