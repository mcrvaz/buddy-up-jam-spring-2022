using System;
using UnityEngine;

[Serializable]
public class WeaponAnimationMappingEntry
{
    [field: SerializeField] public WeaponAnimationId AnimationId { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
}