using UnityEngine;

public delegate void BodyPartCollisionHandler (BodyPart part, Collider collider);

public class BodyPartBehaviour : MonoBehaviour
{
    public event BodyPartCollisionHandler OnBodyPartCollisionEnter;
    public event BodyPartCollisionHandler OnBodyPartCollisionExit;

    public bool Enabled
    {
        get => _collider.enabled;
        set => _collider.enabled = value;
    }

    [field: SerializeField] public BodyPart Part { get; private set; }

    Collider _collider;

    void Awake ()
    {
        _collider = GetComponent<Collider>();
    }

    void OnCollisionEnter (Collision collision) => OnBodyPartCollisionEnter?.Invoke(Part, collision.collider);

    void OnCollisionExit (Collision collision) => OnBodyPartCollisionExit?.Invoke(Part, collision.collider);

    void OnTriggerEnter (Collider collider) => OnBodyPartCollisionEnter?.Invoke(Part, collider);

    void OnTriggerExit (Collider collider) => OnBodyPartCollisionExit?.Invoke(Part, collider);
}