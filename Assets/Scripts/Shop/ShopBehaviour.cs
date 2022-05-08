using System;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;

    [field: SerializeField] public GameObject ShopDoor { get; private set; }

    ItemSaleBehaviour[] itemsOnSale;

    void Awake ()
    {
        itemsOnSale = GetComponentsInChildren<ItemSaleBehaviour>();
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
