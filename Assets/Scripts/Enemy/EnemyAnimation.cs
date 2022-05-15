using System.Collections;
using UnityEngine;

public class EnemyAnimation
{
    const float DEATH_ANIMATION_TIME = 1.5f;
    const float SPAWN_ANIMATION_TIME = 1.5f;

    const string IDLE = "Armature|Skull_Hunting";
    const string STRIKE = "Armature|Skull_Strike";
    const string HIT = "Armature|Skull_Damage";

    readonly Enemy enemy;
    readonly Animation animation;
    readonly Renderer renderer;
    readonly MonoBehaviour coroutineRunner;

    public EnemyAnimation (Enemy enemy, Animation animation, Renderer renderer, MonoBehaviour coroutineRunner)
    {
        this.enemy = enemy;
        this.animation = animation;
        this.renderer = renderer;
        this.coroutineRunner = coroutineRunner;

        enemy.OnDeath += HandleDeath;
        enemy.OnHit += HandleHit;
        enemy.OnCloseToPlayer += HandleCloseToPlayer;
    }

    public void Start ()
    {
        animation.PlayRandomStart(IDLE);
        coroutineRunner.StartCoroutine(SpawnRoutine());
    }

    void HandleCloseToPlayer (Enemy enemy)
    {
        if (!enemy.CanAttack)
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
        coroutineRunner.StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine ()
    {
        float t = 0;
        float ratio;
        while (t < DEATH_ANIMATION_TIME)
        {
            t += Time.deltaTime;
            ratio = t / DEATH_ANIMATION_TIME;
            renderer.material.SetFloat("_Dissolve", ratio);
            yield return null;
        }
    }

    IEnumerator SpawnRoutine ()
    {
        float t = 0;
        float ratio;
        while (t < SPAWN_ANIMATION_TIME)
        {
            t += Time.deltaTime;
            ratio = t / SPAWN_ANIMATION_TIME;
            renderer.material.SetFloat("_Dissolve", 1 - ratio);
            yield return null;
        }
    }
}