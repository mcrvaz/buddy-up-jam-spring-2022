using UnityEngine;

public class PlayerSettings : ScriptableObject
{
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    [field: SerializeField] public HealthSettings HealthSettings { get; private set; }
}