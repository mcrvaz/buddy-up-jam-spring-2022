public class ItemPurchaseFulfillment
{
    readonly PlayerBehaviour player;

    public ItemPurchaseFulfillment (PlayerBehaviour player)
    {
        this.player = player;
    }

    public void GrantReward (ItemSaleSettings settings)
    {
        if (settings.IsAmmoItem)
            GrantAmmo(settings.AmmoItem);
        if (settings.IsHealthItem)
            GrantHealth(settings.HealthItem);
        if (settings.IsWeaponItem)
            GrantWeapon(settings.WeaponItem);
    }

    public bool CanFulfill (ItemSaleSettings settings)
    {
        if (settings.IsAmmoItem)
            return CanGrantAmmo(settings.AmmoItem);
        if (settings.IsHealthItem)
            return CanGrantHealth(settings.HealthItem);
        if (settings.IsWeaponItem)
            return CanGrantWeapon(settings.WeaponItem);
        return false;
    }

    bool CanGrantAmmo (AmmoItem ammoItem)
    {
        var weapon = player.Player.GetWeapon(ammoItem.WeaponId);
        if (!player.PlayerWeapon.HasWeapon(ammoItem.WeaponId))
            return false;
        return weapon.CurrentAmmo < weapon.MaxAmmo;
    }

    bool CanGrantHealth (HealthItem healthItem)
    {
        return player.Health.Current < player.Health.MaxHealth;
    }

    bool CanGrantWeapon (WeaponItem weaponItem)
    {
        return !player.PlayerWeapon.HasWeapon(weaponItem.WeaponId);
    }

    void GrantHealth (HealthItem healthItem)
    {
        player.Player.Health.RestoreHealth(healthItem.Amount);
    }

    void GrantAmmo (AmmoItem ammoItem)
    {
        player.Player.GetWeapon(ammoItem.WeaponId).RestoreAmmo(ammoItem.Amount);
    }

    void GrantWeapon (WeaponItem weaponItem)
    {
        player.PlayerWeapon.EarnWeapon(weaponItem.WeaponId, true);
    }
}