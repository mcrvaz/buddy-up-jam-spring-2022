using System.Collections;
using UnityEngine;

public static class AnimationExtensions
{
    public static IEnumerator PlayAndWait (this Animation animation, string clipName)
    {
        animation.Play(clipName);
        while (animation.isPlaying)
            yield return null;
    }
}