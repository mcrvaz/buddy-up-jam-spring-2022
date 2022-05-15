using System.Collections;
using UnityEngine;

public class LightFadeOut : MonoBehaviour
{
    [SerializeField] float intensity;
    [SerializeField] float fadeTime;
    [SerializeField] Light _light;

    Coroutine fadeRoutine;

    void OnEnable ()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine ()
    {
        float time = 0;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            _light.intensity = Mathf.Lerp(
                intensity,
                0,
                time / fadeTime
            );
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
