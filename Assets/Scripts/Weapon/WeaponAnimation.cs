using UnityEngine;

public class WeaponAnimation
{
    const string SHOOT = "Armature|Shotgun_Shoot";
    const string IDLE = "Armature|Shotgun_Idle";
    const string RELOAD = "Armature|Shotgun_Reload";
    const string SWAP = "Armature|Shotgun_Swap";

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

    public void Start ()
    {
        PlayIdle();
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