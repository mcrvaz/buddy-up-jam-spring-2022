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

    public static void PlayRandomStart (this Animation animation, string clipName)
    {
        AnimationState animationState = animation[clipName];
        animation.Play(clipName);
        animationState.time = Random.Range(0f, animationState.length);
    }
}