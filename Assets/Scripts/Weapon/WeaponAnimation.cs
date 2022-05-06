using UnityEngine;

public class WeaponAnimation
{
    public const string SHOOT = "";
    public const string IDLE = "Armature|Shotgun_Idle";
    public const string RELOAD = "Armature|Shotgun_Reload";

    readonly Weapon weapon;
    readonly Animation animation;

    public WeaponAnimation (Weapon weapon, Animation animation)
    {
        this.weapon = weapon;
        this.animation = animation;
        weapon.OnShoot += HandleShoot;
        weapon.OnReloadEnd += HandleReloadEnd;
        weapon.OnReloadStart += HandleReloadStart;
    }

    void HandleShoot ()
    {
        // animation.Play(SHOOT);
    }

    void HandleReloadEnd ()
    {
        // PlayIdle();
    }

    void PlayIdle ()
    {
        animation.Play(IDLE);
    }

    void HandleReloadStart ()
    {
        animation.Play(RELOAD);
        animation.PlayQueued(IDLE);
    }
}