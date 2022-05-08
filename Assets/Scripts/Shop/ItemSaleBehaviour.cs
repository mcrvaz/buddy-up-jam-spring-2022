using System;
using TMPro;
using UnityEngine;

public class ItemSaleBehaviour : MonoBehaviour
{
    public event Action<ItemSaleBehaviour, bool> OnItemSaleActiveChanged;

    [field: SerializeField] public TextMeshPro PriceText { get; private set; }
    [field: SerializeField] public ItemSaleSettings Settings { get; private set; }

    void Start ()
    {

    }

    void Update ()
    {

    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out _))
            OnItemSaleActiveChanged?.Invoke(this, true);
    }

    void OnTriggerExit (Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out _))
            OnItemSaleActiveChanged?.Invoke(this, false);
    }
}
