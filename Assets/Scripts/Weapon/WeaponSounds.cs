using System;
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
        weapon.OnReloadStart += HandleWeaponReloadStart;
        weapon.OnEmptyAmmoFire += HandleEmptyAmmoFire;
    }

    void HandleEmptyAmmoFire ()
    {
        audioManager.PlayEmptyAmmo(audioSource);
    }

    void HandleWeaponReloadStart (float obj)
    {
        audioManager.PlayShotgunReload(audioSource);
    }

    void HandleWeaponShoot ()
    {
        audioManager.PlayShotgunFire(audioSource);
    }
}