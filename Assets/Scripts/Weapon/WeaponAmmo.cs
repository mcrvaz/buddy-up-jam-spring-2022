using System;
using UnityEngine;

public class WeaponAmmo
{
    public event Action OnAmmoUpdated;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => settings.MaxAmmo;
    public int RoundsInClip { get; private set; }
    public int RoundsPerClip => settings.RoundsPerClip;
    public int Damage => settings.Damage;

    readonly IWeaponSettings settings;

    public WeaponAmmo (IWeaponSettings settings)
    {
        this.settings = settings;
    }

    public void SetupInitialAmmo ()
    {
        CurrentAmmo = settings.StartingAmmo;
        ReloadClip();
        OnAmmoUpdated?.Invoke();
    }

    public void RestoreAmmo (int amount)
    {
        CurrentAmmo = Mathf.Min(CurrentAmmo + amount, MaxAmmo);
        OnAmmoUpdated?.Invoke();
    }

    public bool ConsumeAmmo ()
    {
        if (RoundsInClip <= 0)
            return false;

        RoundsInClip--;
        OnAmmoUpdated?.Invoke();
        return true;
    }

    public void OverrideCurrentAmmo (int ammo)
    {
        CurrentAmmo = ammo;
        OnAmmoUpdated?.Invoke();
    }

    public void OverrideRoundsInClip (int ammo)
    {
        RoundsInClip = ammo;
        OnAmmoUpdated?.Invoke();
    }

    public void ReloadClip ()
    {
        if (RoundsPerClip == RoundsInClip)
            return;

        int bulletsToReload;
        if (CurrentAmmo > RoundsPerClip)
            bulletsToReload = RoundsPerClip - RoundsInClip;
        else
        {
            if (CurrentAmmo + RoundsInClip > RoundsPerClip)
                bulletsToReload = RoundsPerClip - RoundsInClip;
            else
                bulletsToReload = CurrentAmmo;
        }

        RoundsInClip += bulletsToReload;
        CurrentAmmo = Mathf.Max(0, CurrentAmmo - bulletsToReload);

        OnAmmoUpdated?.Invoke();
    }
}