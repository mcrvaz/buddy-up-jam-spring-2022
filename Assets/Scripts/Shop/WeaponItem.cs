using System;
using UnityEngine;

[Serializable]
public class WeaponItem
{
    [field: SerializeField] public WeaponId WeaponId { get; private set; }
}