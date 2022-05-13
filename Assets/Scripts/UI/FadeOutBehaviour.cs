using UnityEngine;

public class FadeOutBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject FadeOut { get; private set; }

    void Awake ()
    {
        FadeOut.SetActive(true);
    }
}