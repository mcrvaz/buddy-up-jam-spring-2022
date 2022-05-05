using UnityEngine;

public class EnemySpawnerWaveSettings : ScriptableObject
{
    [field: SerializeField] public int EnemyCount { get; private set; }
    [field: SerializeField] public AnimationCurve SpawnRate { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public EnemyChanceSettings[] EnemyChances { get; private set; }
}