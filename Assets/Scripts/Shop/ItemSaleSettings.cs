using UnityEngine;

public class ItemSaleSettings : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}
