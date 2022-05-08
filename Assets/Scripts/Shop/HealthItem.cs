using System;
using UnityEngine;

[Serializable]
public class HealthItem
{
    [field: SerializeField] public int Amount { get; private set; }
}