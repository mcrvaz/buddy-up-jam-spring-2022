using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation
{
    readonly Weapon weapon;
    readonly Animation animation;
    readonly float reloadAnimationDuration;
    readonly float swapInAnimationDuration;
    readonly float swapOutAnimationDuration;
    readonly IReadOnlyList<ParticleSystem> particles;
    readonly WeaponAnimationMapping animationMapping;

    public WeaponAnimation (
        Weapon weapon,
        Animation animation,
        IReadOnlyList<ParticleSystem> particles,
        WeaponAnimationMapping animationMapping
    )
    {
        this.weapon = weapon;
        this.animation = animation;
        this.particles = particles;
        this.animationMapping = animationMapping;
        reloadAnimationDuration = animation[GetAnimationName(WeaponAnimationId.Reload)].length;
        swapInAnimationDuration = animation[GetAnimationName(WeaponAnimationId.SwapIn)].length;
        swapOutAnimationDuration = animation[GetAnimationName(WeaponAnimationId.SwapOut)].length;
    }

    public void Start ()
    {
        weapon.OnShoot += HandleShoot;
        weapon.OnReloadStart += HandleReloadStart;
        weapon.OnSwapInStart += HandleWeaponSwapInStart;
        weapon.OnSwapOutStart += HandleWeaponSwapOutStart;
        PlayIdle();
    }

    void HandleShoot ()
    {
        animation.Stop();
        animation.Play(GetAnimationName(WeaponAnimationId.Shoot));
        animation.PlayQueued(GetAnimationName(WeaponAnimationId.Idle));
    }

    void PlayIdle ()
    {
        animation.Play(GetAnimationName(WeaponAnimationId.Idle));
    }

    void HandleReloadStart (float reloadDuration)
    {
        animation.Stop();
        var animationName = GetAnimationName(WeaponAnimationId.Reload);
        animation[animationName].speed = GetSpeedMultiplier(reloadDuration, reloadAnimationDuration);
        animation.Play(animationName);
        animation.PlayQueued(GetAnimationName(WeaponAnimationId.Idle));
    }

    void HandleWeaponSwapOutStart (float duration)
    {
        animation.Stop();
        var animationName = GetAnimationName(WeaponAnimationId.SwapOut);
        animation[animationName].speed = GetSpeedMultiplier(duration, swapOutAnimationDuration);
        animation.Play(animationName);
    }

    void HandleWeaponSwapInStart (float duration)
    {
        animation.Stop();
        var animationName = GetAnimationName(WeaponAnimationId.SwapIn);
        animation.Play(GetAnimationName(WeaponAnimationId.SwapIn));
        animation[animationName].speed = GetSpeedMultiplier(duration, swapInAnimationDuration);
        animation.PlayQueued(GetAnimationName(WeaponAnimationId.Idle));
    }

    float GetSpeedMultiplier (float actionDuration, float animationDuration) =>
        animationDuration / actionDuration;

    string GetAnimationName (WeaponAnimationId animationId) =>
        animationMapping.GetClipName(animationId);
}