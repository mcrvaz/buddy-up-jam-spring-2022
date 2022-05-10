using System;

public class ItemSale
{
    public event Action<ItemSale> OnPurchase;

    readonly ItemSaleSettings settings;
    readonly Currency currency;
    readonly InputManager inputManager;
    readonly ItemPurchaseFulfillment fulfillment;

    public ItemSale (
        InputManager inputManager,
        ItemPurchaseFulfillment fulfillment,
        ItemSaleSettings settings,
        Currency currency
    )
    {
        this.inputManager = inputManager;
        this.fulfillment = fulfillment;
        this.settings = settings;
        this.currency = currency;
    }

    public void Update ()
    {
        if (inputManager.GetConfirmDown())
            PurchaseActiveItem();
    }

    void PurchaseActiveItem ()
    {
        if (!currency.Spend(settings.Price))
            return;
        fulfillment.GrantReward(settings);
        OnPurchase?.Invoke(this);
    }
}