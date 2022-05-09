using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [field: SerializeField] public CameraShakeDatabaseSettings ShakeSettings { get; private set; }

    float speed;
    float maxMagnitude;
    float noiseMagnitude;
    float time;
    Vector2 direction;
    Coroutine shakeRoutine;
    float fadeOut = 0f;

    public void PlayStrongShake () => FireOnce(ShakeSettings.StrongShake);

    public void PlayHitShake () => FireOnce(ShakeSettings.HitShake);

    public void FireOnce (CustomShake customShake)
    {
        speed = customShake.Speed;
        maxMagnitude = customShake.MaxMagnitude;
        noiseMagnitude = customShake.NoiseMagnitude;
        time = customShake.Time;
        direction = customShake.Direction;

        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(ShakeAndStop(time));
    }

    IEnumerator ShakeAndStop (float fadeDuration)
    {
        fadeOut = 1f;
        float sin;
        Vector2 newDirection;
        float fadeOutStart = Time.time;
        float fadeOutComplete = fadeOutStart + fadeDuration;

        float time = Time.time;
        while (time < fadeOutComplete)
        {
            time = Time.time;
            fadeOut = 1f - Mathf.InverseLerp(fadeOutStart, fadeOutComplete, time);
            sin = Mathf.Sin(speed * time);
            newDirection = direction + GetNoise(time);
            newDirection.Normalize();
            gameObject.transform.localPosition = newDirection * sin * maxMagnitude * fadeOut;
            yield return null;
        }
    }

    Vector2 GetNoise (in float time) =>
        noiseMagnitude * new Vector2(time - 0.5f, time - 0.5f);
}
