using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSettings : ScriptableObject
{
    [field: SerializeField] public List<EnemySpawnerWaveSettings> Waves { get; private set; }
}