using System;
using UnityEngine;

[Serializable]
public class BodyPartDamageSettingsEntry
{
    [field: SerializeField] public BodyPart BodyPart { get; set; }
    [field: SerializeField] public float DamageMultiplier { get; set; } = 1f;
}