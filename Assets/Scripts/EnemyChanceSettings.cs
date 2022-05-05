using System;
using UnityEngine;

[Serializable]
public class EnemyChanceSettings
{
    [field: SerializeField] public EnemyBehaviour EnemyPrefab { get; private set; }
    [field: SerializeField] public float Chance { get; private set; }
}