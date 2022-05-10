using UnityEngine;

public class WeaponSounds
{
    readonly Weapon weapon;
    readonly AudioManager audioManager;
    readonly AudioSource audioSource;

    public WeaponSounds (
        Weapon weapon,
        AudioManager audioManager,
        AudioSource audioSource
    )
    {
        this.weapon = weapon;
        this.audioManager = audioManager;
        this.audioSource = audioSource;
        weapon.OnShoot += HandleWeaponShoot;
    }

    void HandleWeaponShoot ()
    {
        audioManager.PlayShotgunFire(audioSource);
    }
}