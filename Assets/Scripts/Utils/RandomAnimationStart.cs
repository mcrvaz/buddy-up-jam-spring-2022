using UnityEngine;

[RequireComponent(typeof(Animation))]
public class RandomAnimationStart : MonoBehaviour
{
    void OnEnable ()
    {
        Animation anim = GetComponent<Animation>();
        AnimationClip currentClip = anim.clip;
        AnimationState animationState = anim[currentClip.name];
        animationState.time = Random.Range(0f, animationState.length);
    }
}
