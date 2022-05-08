using UnityEngine;

public class CameraShakeDatabaseSettings : ScriptableObject
{
    [field: SerializeField] public CustomShake StrongShake { get; private set; }
}