using UnityEngine;

public class ProjectileSettings : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 1f;
    [field: SerializeField] public float TimeToLive { get; private set; } = 1f;
    [field: SerializeField] public int PenetrationCount { get; private set; } = 1;
}