using System;
using UnityEngine;

[Serializable]
public class AmmoItem
{
    [field: SerializeField] public WeaponId WeaponId { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}