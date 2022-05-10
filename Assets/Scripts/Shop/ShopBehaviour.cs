using System;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;
    public event Action<ItemSaleBehaviour> OnPurchase;

    [field: SerializeField] public GameObject ShopDoor { get; private set; }

    public ItemSale ItemSale { get; private set; }
    public ItemPurchaseFulfillment PurchaseFulfillment { get; private set; }

    ShopSounds shopSounds;
    GameAudioBehaviour audioBehaviour;
    PlayerBehaviour playerBehaviour;
    ItemSaleBehaviour[] itemsOnSale;

    void Awake ()
    {
        audioBehaviour = FindObjectOfType<GameAudioBehaviour>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        itemsOnSale = GetComponentsInChildren<ItemSaleBehaviour>();
        PurchaseFulfillment = new ItemPurchaseFulfillment(playerBehaviour);
    }

    void Start ()
    {
        foreach (var item in itemsOnSale)
        {
            item.OnPurchase += RaiseOnPurchase;
            item.OnItemSaleActiveChanged += HandleItemSaleActive;
        }
        shopSounds = new ShopSounds(this, audioBehaviour.AudioManager);
    }

    void RaiseOnPurchase (ItemSaleBehaviour sale) => OnPurchase?.Invoke(sale);

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active)
    {
        OnItemSaleActiveChanged?.Invoke(itemSale, active);
    }
}
