using System;
using VContainer.Unity;

public class AmmoCounter : IInitializable, IDisposable
{
    public event Action<string> OnValueChanged;

    public string Value => $"{activeWeapon.RoundsInClip}/{activeWeapon.CurrentAmmo}";

    readonly PlayerWeapon playerWeapon;

    IWeapon activeWeapon;

    public AmmoCounter (PlayerWeapon playerWeapon)
    {
        this.playerWeapon = playerWeapon;
    }

    public void Initialize ()
    {
        playerWeapon.OnWeaponChanged += HandleWeaponChanged;
    }

    void HandleWeaponChanged ()
    {
        if (activeWeapon != null)
            activeWeapon.OnAmmoUpdated -= UpdateAmmoCounter;

        activeWeapon = playerWeapon.ActiveWeapon;
        activeWeapon.OnAmmoUpdated += UpdateAmmoCounter;
        UpdateAmmoCounter();
    }

    void UpdateAmmoCounter ()
    {
        OnValueChanged?.Invoke(Value);
    }

    public void Dispose ()
    {
        playerWeapon.OnWeaponChanged -= HandleWeaponChanged;
        if (activeWeapon != null)
            activeWeapon.OnAmmoUpdated -= UpdateAmmoCounter;
    }
}