using UnityEngine;

public class ItemSaleSettings : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    [field:SerializeField] public bool IsHealthItem { get; private set; }
    [field:SerializeField] public HealthItem HealthItem { get; private set; }
    [field:SerializeField] public bool IsAmmoItem { get; private set; }
    [field:SerializeField] public AmmoItem AmmoItem { get; private set; }
    [field:SerializeField] public bool IsWeaponItem { get; private set; }
    [field:SerializeField] public WeaponItem WeaponItem { get; private set; }
}
