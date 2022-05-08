using UnityEngine;

public class CustomShake : ScriptableObject
{
    [SerializeField]
    public string CustomShakeName;

    [SerializeField]
    [Range(0.01f, 50f)]
    public float Speed = 20f;

    [SerializeField]
    [Tooltip("We won't move further than this distance from neutral.")]
    [Range(0.01f, 10f)]
    public float MaxMagnitude = 0.3f;

    [SerializeField]
    [Tooltip("0 follows _Direction exactly. 3 mostly ignores _Direction and shakes in all directions.")]
    [Range(0f, 3f)]
    public float NoiseMagnitude = 0.3f;

    [SerializeField]
    [Range(0f, 10f)]
    public float Time = 0.5f;

    [SerializeField]
    public Vector2 Direction = Vector2.up;
}
