using UnityEngine;

public class EnemySounds
{
    readonly Enemy enemy;
    readonly AudioManager audioManager;
    readonly AudioSource audioSource;

    int lastFramePlayedHit;

    public EnemySounds (Enemy enemy, AudioManager audioManager, AudioSource audioSource)
    {
        this.enemy = enemy;
        this.audioManager = audioManager;
        this.audioSource = audioSource;

        enemy.OnHit += HandleHit;
        enemy.OnDeath += HandleDeath;
    }

    void HandleDeath (Enemy enemy)
    {
        audioManager.PlayEnemyDeath(audioSource);
    }

    void HandleHit (Enemy enemy, BodyPart bodyPart)
    {
        var currentFrame = Time.frameCount;
        if (currentFrame == lastFramePlayedHit)
            return;
        lastFramePlayedHit = Time.frameCount;
        audioManager.PlayEnemyHit(audioSource);
    }
}