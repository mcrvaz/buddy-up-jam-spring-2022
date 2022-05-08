using UnityEngine;

public class FadeOutBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject FadeOut;

    void Awake ()
    {
        FadeOut.SetActive(true);
    }
}