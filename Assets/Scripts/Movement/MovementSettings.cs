using UnityEngine;

public class MovementSettings : ScriptableObject
{
    [field: SerializeField] public float FlyingSpeedModifier { get; set; }
    [field: SerializeField] public float ForwardSpeed { get; set; }
    [field: SerializeField] public float BackwardSpeed { get; set; }
    [field: SerializeField] public int MaxJumpCount { get; set; }
    [field: SerializeField] public float GravityModifier { get; set; }
    [field: SerializeField] public float Acceleration { get; set; }
    [field: SerializeField] public float JumpForce { get; set; }
}