using UnityEngine;

public class WeaponAnimation
{
    public const string SHOOT = "Armature|Shotgun_Shoot";
    public const string IDLE = "Armature|Shotgun_Idle";
    public const string RELOAD = "Armature|Shotgun_Reload";
    public const string SWAP = "Armature|Shotgun_Swap";

    readonly Weapon weapon;
    readonly Animation animation;
    readonly float reloadAnimationDuration;

    public WeaponAnimation (Weapon weapon, Animation animation)
    {
        this.weapon = weapon;
        this.animation = animation;
        weapon.OnShoot += HandleShoot;
        weapon.OnReloadStart += HandleReloadStart;

        reloadAnimationDuration = animation[RELOAD].length;
    }

    void HandleShoot ()
    {
        animation.Play(SHOOT);
    }

    void PlayIdle ()
    {
        animation.Play(IDLE);
    }

    void HandleReloadStart (float reloadDuration)
    {
        float ratio = reloadAnimationDuration / reloadDuration;
        animation[RELOAD].speed = ratio;
        animation.Play(RELOAD);
        animation.PlayQueued(IDLE);
    }
}