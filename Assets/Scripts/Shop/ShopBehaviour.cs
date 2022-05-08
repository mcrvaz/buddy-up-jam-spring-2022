using System;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;

    [field: SerializeField] public GameObject ShopDoor { get; private set; }

    public ItemSale ItemSale { get; private set; }
    public ItemPurchaseFulfillment PurchaseFulfillment { get; private set; }

    PlayerBehaviour playerBehaviour;
    ItemSaleBehaviour[] itemsOnSale;

    void Awake ()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        itemsOnSale = GetComponentsInChildren<ItemSaleBehaviour>();
        PurchaseFulfillment = new ItemPurchaseFulfillment(playerBehaviour);
    }

    void Start ()
    {
        foreach (var item in itemsOnSale)
            item.OnItemSaleActiveChanged += HandleItemSaleActive;
    }

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active)
    {
        OnItemSaleActiveChanged?.Invoke(itemSale, active);
    }
}
