using UnityEngine;

public class ShotgunWeaponSounds : IWeaponSounds
{
    readonly AudioManager audioManager;
    readonly AudioSource audioSource;

    public ShotgunWeaponSounds (AudioManager audioManager, AudioSource audioSource)
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
        audioManager.PlayShotgunReload(audioSource);
    }

    public void Shoot ()
    {
        audioManager.PlayShotgunFire(audioSource);
    }

    public void SwapIn ()
    {
        audioManager.PlayShotgunSwapIn(audioSource);
    }

    public void SwapOut ()
    {
        audioManager.PlayShotgunSwapOut(audioSource);
    }
}