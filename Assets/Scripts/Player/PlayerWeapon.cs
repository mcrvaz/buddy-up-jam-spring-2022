using System;
using System.Collections.Generic;

public class PlayerWeapon
{
    public event Action OnWeaponChanged;

    public IWeapon ActiveWeapon { get; private set; }
    public int HandleSwapOutEnd { get; private set; }

    readonly List<WeaponId> acquiredWeapons = new List<WeaponId>();
    readonly IReadOnlyList<IWeaponBehaviour> weaponBehaviours;
    readonly PlayerSettings settings;

    IWeapon newWeapon;

    public PlayerWeapon (
        IReadOnlyList<IWeaponBehaviour> weaponBehaviours,
        PlayerSettings settings
    )
    {
        this.weaponBehaviours = weaponBehaviours;
        this.settings = settings;
        SetupWeapons();
    }

    public void EarnWeapon (WeaponId weaponId, bool autoSwitch)
    {
        acquiredWeapons.Add(weaponId);
        if (autoSwitch)
            ChangeWeapon(weaponId);
    }

    public void ChangeWeapon (int weaponIndex)
    {
        weaponIndex--;
        if (weaponIndex >= acquiredWeapons.Count)
            return;
        ChangeWeapon(acquiredWeapons[weaponIndex]);
    }

    public void ChangeWeapon (WeaponId weaponId)
    {
        if (!acquiredWeapons.Contains(weaponId))
            return;

        var currentWeapon = ActiveWeapon;
        newWeapon = GetWeapon(weaponId);
        if (currentWeapon == newWeapon)
            return;
        currentWeapon.SwapOut();
    }

    public IWeapon GetWeapon (WeaponId weaponId)
    {
        foreach (var item in weaponBehaviours)
        {
            if (item.Weapon.WeaponId == weaponId)
                return item.Weapon;
        }
        return null;
    }

    void SetupWeapons ()
    {
        var startingWeapon = settings.StartingWeapons[0];
        foreach (WeaponId weaponId in settings.StartingWeapons)
        {
            EarnWeapon(weaponId, false);
            var weapon = GetWeapon(weaponId);
            weapon.OnSwapInEnded += HandleSwapInEnded;
            weapon.OnSwapOutEnded += HandleSwapOutEnded;
        }
        SetWeaponActive(startingWeapon);
    }

    void HandleSwapInEnded (IWeapon weapon)
    {
        foreach (var item in weaponBehaviours)
        {
            if (item.Weapon.WeaponId == weapon.WeaponId)
                item.SetActive(true);
        }
        SetWeaponActive(weapon.WeaponId);
    }

    void HandleSwapOutEnded (IWeapon weapon)
    {
        foreach (var item in weaponBehaviours)
        {
            if (item.Weapon.WeaponId == weapon.WeaponId)
                item.SetActive(false);
        }
        newWeapon.SwapIn();
    }

    void SetWeaponActive (WeaponId weaponId)
    {
        if (!acquiredWeapons.Contains(weaponId))
            return;
        foreach (var item in weaponBehaviours)
            item.SetActive(item.Weapon.WeaponId == weaponId);
        ActiveWeapon = GetWeapon(weaponId);
        ActiveWeapon.SetAsActive();
        OnWeaponChanged?.Invoke();
    }
}