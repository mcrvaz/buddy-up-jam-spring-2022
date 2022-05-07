using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void SetActive (this MonoBehaviour behaviour, bool active)
    {
        behaviour.gameObject.SetActive(active);
    }
}