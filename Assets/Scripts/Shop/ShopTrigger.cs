using System;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerExitEvent;

    void OnTriggerEnter (Collider collider) =>
        OnTriggerEnterEvent?.Invoke(collider);

    void OnTriggerExit (Collider collider) =>
        OnTriggerExitEvent?.Invoke(collider);
}
