using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [field: SerializeField] public Camera Camera { get; private set; }
    [field: SerializeField] public CameraShake CameraShake { get; private set; }
}