using UnityEngine;

public class HealthSettings : ScriptableObject
{
    [field: SerializeField] public float InitialHealth { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float InvulnerabilityTime { get; private set; }
    [field: SerializeField] public BodyPartDamageSettings BodyPartDamageSettings { get; private set; }
}