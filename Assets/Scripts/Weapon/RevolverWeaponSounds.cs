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
        audioManager.PlayRevolverReload(audioSource);
    }

    public void Shoot ()
    {
        audioManager.PlayRevolverFire(audioSource);
    }

    public void SwapIn ()
    {
        audioManager.PlayRevolverSwapIn(audioSource);
    }

    public void SwapOut ()
    {
        audioManager.PlayRevolverSwapOut(audioSource);
    }
}