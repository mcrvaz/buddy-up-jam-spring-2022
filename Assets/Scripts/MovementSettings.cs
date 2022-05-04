using UnityEngine;

public class MovementSettings : ScriptableObject
{
    [field:SerializeField] public float ForwardSpeed { get; set; }
    [field:SerializeField] public float BackwardSpeed { get; set; }
    [field:SerializeField] public float JumpSpeed { get; set; }
    [field:SerializeField] public float JumpDuration { get; set; }
    [field:SerializeField] public int MaxJumpCount { get; set; }
    [field:SerializeField] public float GravityModifier { get; set; } = 1f;
}