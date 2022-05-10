using System;
using TMPro;
using UnityEngine;

public class ItemSaleBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;
    public event Action<ItemSaleBehaviour> OnPurchase;

    [field: SerializeField] public TextMeshPro PriceText { get; private set; }
    [field: SerializeField] public ItemSaleSettings Settings { get; private set; }

    public ItemSale ItemSale { get; private set; }

    ShopBehaviour shopBehaviour;
    CurrencyManager currencyManager;

    void Awake ()
    {
        shopBehaviour = FindObjectOfType<ShopBehaviour>();
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    void Start ()
    {
        ItemSale = new ItemSale(
            InputManager.Instance,
            shopBehaviour.PurchaseFulfillment,
            Settings,
            currencyManager.Currency
        );
        ItemSale.OnPurchase += RaiseOnPurchase;
        PriceText.text = $"${Settings.Price}";
        enabled = false;
    }

    void Update ()
    {
        ItemSale.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out _))
        {
            enabled = true;
            OnItemSaleActiveChanged?.Invoke(this, true);
        }
    }

    void OnTriggerExit (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out _))
        {
            enabled = false;
            OnItemSaleActiveChanged?.Invoke(this, false);
        }
    }

    void RaiseOnPurchase (ItemSale obj) => OnPurchase?.Invoke(this);
}
