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