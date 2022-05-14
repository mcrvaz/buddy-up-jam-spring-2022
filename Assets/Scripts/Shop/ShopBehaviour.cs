using System;
using UnityEngine;

public class ShopBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;
    public event Action<ItemSaleBehaviour> OnPurchase;

    [field: SerializeField] public GameObject ShopDoor { get; set; }
    [field: SerializeField] public Transform ShopExit { get; set; }

    public ItemSale ItemSale { get; private set; }
    public ItemPurchaseFulfillment PurchaseFulfillment { get; private set; }

    ShopSounds shopSounds;
    ShopTrigger shopTrigger;
    GameAudioBehaviour audioBehaviour;
    PlayerBehaviour playerBehaviour;
    ItemSaleBehaviour[] itemsOnSale;
    EnemyWaveManagerBehaviour waveManagerBehaviour;
    FadeOutBehaviour fadeOut;

    bool playerInsideShop;

    void Awake ()
    {
        shopTrigger = GetComponentInChildren<ShopTrigger>();
        audioBehaviour = FindObjectOfType<GameAudioBehaviour>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        itemsOnSale = GetComponentsInChildren<ItemSaleBehaviour>();
        waveManagerBehaviour = FindObjectOfType<EnemyWaveManagerBehaviour>();
        fadeOut = FindObjectOfType<FadeOutBehaviour>();
        PurchaseFulfillment = new ItemPurchaseFulfillment(playerBehaviour);

        shopTrigger.OnTriggerEnterEvent += HandleTriggerEnter;
        shopTrigger.OnTriggerExitEvent += HandleTriggerExit;
    }

    void Start ()
    {
        foreach (var item in itemsOnSale)
        {
            item.OnPurchase += RaiseOnPurchase;
            item.OnItemSaleActiveChanged += HandleItemSaleActive;
        }
        shopSounds = new ShopSounds(this, audioBehaviour.AudioManager);

        waveManagerBehaviour.WaveManager.OnWaveEnded += HandleWaveEnded;
        waveManagerBehaviour.WaveManager.OnWaveStarted += HandleWaveStarted;
        bool waveInProgress = waveManagerBehaviour.WaveManager.IsWaveInProgress;
        if (waveInProgress)
            HandleWaveStarted();
        else
            HandleWaveEnded();
    }

    void HandleWaveStarted ()
    {
        enabled = false;
        ShopDoor.SetActive(true);
        TeleportPlayerOutOfShop();
    }

    void HandleWaveEnded ()
    {
        enabled = true;
        ShopDoor.SetActive(false);
    }

    void TeleportPlayerOutOfShop ()
    {
        if (!playerInsideShop)
            return;
        playerBehaviour.Player.Teleport(ShopExit);
        fadeOut.PlayFadeOut();
    }

    void RaiseOnPurchase (ItemSaleBehaviour sale) => OnPurchase?.Invoke(sale);

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active) =>
        OnItemSaleActiveChanged?.Invoke(itemSale, active);

    void HandleTriggerEnter (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out var _))
            playerInsideShop = true;
    }

    void HandleTriggerExit (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out var _))
            playerInsideShop = false;
    }
}
