using UnityEngine;

public class EnemyAnimation
{
    const string IDLE = "Armature|Skull_Hunting";
    const string STRIKE = "Armature|Skull_Strike";
    const string HIT = "Armature|Skull_Damage";

    readonly Enemy enemy;
    readonly Animation animation;

    public EnemyAnimation (Enemy enemy, Animation animation)
    {
        this.enemy = enemy;
        this.animation = animation;

        enemy.OnDeath += HandleDeath;
        enemy.OnHit += HandleHit;
        enemy.OnCloseToPlayer += HandleCloseToPlayer;
    }

    public void Start ()
    {
        animation.PlayRandomStart(IDLE);
    }

    void HandleCloseToPlayer (Enemy enemy)
    {
        if (animation.IsPlaying(STRIKE))
            return;
        animation.Play(STRIKE);
        animation.PlayQueued(IDLE);
    }

    void HandleHit (Enemy enemy, BodyPart bodyPart)
    {
        animation.Play(HIT);
        animation.PlayQueued(IDLE);
    }

    void HandleDeath (Enemy enemy)
    {
        animation.Stop();
        animation.enabled = false;
    }
}