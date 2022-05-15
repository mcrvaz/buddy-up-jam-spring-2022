using System.Collections;
using UnityEngine;

public class FadeOutBehaviour : MonoBehaviour
{
    const string FADE_OUT = "ScreenFadeOut";

    [field: SerializeField] public GameObject FadeOut { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }

    Coroutine fadeRoutine;

    void Awake ()
    {
        PlayFadeOut();
    }

    public void PlayFadeOut ()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        FadeOut.SetActive(true);
        fadeRoutine = StartCoroutine(PlayAndDisable());
    }

    IEnumerator PlayAndDisable ()
    {
        yield return Animation.PlayAndWait(FADE_OUT);
        FadeOut.SetActive(false);
    }
}