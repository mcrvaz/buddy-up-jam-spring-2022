using UnityEngine;

public class RevolverWeaponSounds : IWeaponSounds
{
    readonly AudioManager audioManager;
    readonly AudioSource audioSource;

    public RevolverWeaponSounds (AudioManager audioManager, AudioSource audioSource)
    {
        this.audioManager = audioManager;
        this.audioSource = audioSource;
    }

    public void EmptyClip ()
    {
        audioManager.PlayEmptyAmmo(audioSource);
    }

    public void Reload ()
    {
        audioManager.PlayEmptyAmmo(audioSource);
    }

    public void Shoot ()
    {
        audioManager.PlayShotgunFire(audioSource);
    }

    public void Swap ()
    {
        throw new System.NotImplementedException();
    }
}