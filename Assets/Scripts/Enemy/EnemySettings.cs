using UnityEngine;

public class EnemySettings : ScriptableObject
{
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    [field: SerializeField] public HealthSettings HealthSettings { get; private set; }
    [field: SerializeField] public float PauseTimeAfterHit { get; private set; }
    [field: SerializeField] public float PushForceOnHit { get; private set; }
    [field: SerializeField] public int CurrencyReward { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}