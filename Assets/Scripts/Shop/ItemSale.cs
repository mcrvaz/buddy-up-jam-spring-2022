using System;

public class ItemSale
{
    public event Action<ItemSale> OnPurchase;

    public bool Enabled { get; set; }

    readonly ItemSaleSettings settings;
    readonly CurrencyManager currency;
    readonly InputManager inputManager;
    readonly ItemPurchaseFulfillment fulfillment;

    public ItemSale (
        InputManager inputManager,
        ItemPurchaseFulfillment fulfillment,
        ItemSaleSettings settings,
        CurrencyManager currency
    )
    {
        this.inputManager = inputManager;
        this.fulfillment = fulfillment;
        this.settings = settings;
        this.currency = currency;
    }

    public void Update ()
    {
        if (!Enabled)
            return;

        if (inputManager.GetConfirmDown())
            PurchaseActiveItem();
    }

    void PurchaseActiveItem ()
    {
        if (!fulfillment.CanFulfill(settings))
            return;
        if (!currency.Spend(settings.Price))
            return;
        fulfillment.GrantReward(settings);
        OnPurchase?.Invoke(this);
    }
}