using System;
using TMPro;
using UnityEngine;

public class BuyItemTextBehaviour : MonoBehaviour
{
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

    void HandleItemSaleActive (ItemSaleBehaviour obj, bool active)
    {
        BuyItemText.SetActive(active);
    }
}