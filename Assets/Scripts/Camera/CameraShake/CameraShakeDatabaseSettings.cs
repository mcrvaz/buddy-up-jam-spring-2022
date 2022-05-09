using UnityEngine;

public class CameraShakeDatabaseSettings : ScriptableObject
{
    [field: SerializeField] public CustomShake StrongShake { get; private set; }
    [field: SerializeField] public CustomShake HitShake { get; private set; }
}