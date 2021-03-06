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
            FindObjectOfType<CoroutineRunner>(),
            playerBehaviour.WeaponCamera,
            playerBehaviour.MainCamera.CameraShake
        );
    }

    protected override IWeaponSounds CreateWeaponSounds ()
    {
        return new ShotgunWeaponSounds(
            audioBehaviour.AudioManager,
            playerBehaviour.PlayerAudioSource
        );
    }
}