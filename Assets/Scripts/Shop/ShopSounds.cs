using System;
using UnityEngine;
public class ShopSounds
{
    readonly ShopBehaviour shop;
    readonly AudioManager audioManager;

    public ShopSounds (ShopBehaviour shop, AudioManager audioManager)
    {
        this.shop = shop;
        this.audioManager = audioManager;
        shop.OnPurchase += HandlePurchase;
    }

    void HandlePurchase (ItemSaleBehaviour _) =>
        audioManager.PlayShopPurchase(null);
}