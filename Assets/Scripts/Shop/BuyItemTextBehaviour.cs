using System;
using TMPro;
using UnityEngine;

public class BuyItemTextBehaviour : MonoBehaviour
{
    const string TEXT = "Press E to buy {0} for ${1}";

    [field: SerializeField] public TextMeshProUGUI BuyItemText { get; private set; }

    ShopBehaviour shopBehaviour;

    void Awake ()
    {
        shopBehaviour = FindObjectOfType<ShopBehaviour>();
    }

    void Start ()
    {
        shopBehaviour.OnItemSaleActiveChanged += HandleItemSaleActive;
    }

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active)
    {
        BuyItemText.text = string.Format(TEXT, itemSale.Settings.Name, itemSale.Settings.Price);
        BuyItemText.SetActive(active);
    }
}