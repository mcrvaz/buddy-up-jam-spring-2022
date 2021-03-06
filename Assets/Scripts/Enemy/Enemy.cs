using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    const float PLAYER_DISTANCE_THRESHOLD = 1.5f;
    const float PLAYER_DISTANCE_THRESHOLD_SQUARED =
        PLAYER_DISTANCE_THRESHOLD * PLAYER_DISTANCE_THRESHOLD;

    public event Action<Enemy> OnCloseToPlayer;
    public event Action<Enemy> OnDeath;
    public event Action<Enemy, BodyPart> OnHit;

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }

    public int Damage => settings.Damage;
    public int CurrencyReward => settings.CurrencyReward;
    public float PushForceOnHit => settings.PushForceOnHit;
    public bool CanAttack => Time.time >= nextAttackTime;

    readonly IReadOnlyList<BodyPartBehaviour> bodyParts;
    readonly EnemySettings settings;
    readonly PlayerBehaviour player;
    readonly Transform transform;

    float nextAttackTime = float.MinValue;

    public Enemy (
        Transform transform,
        EnemyMovement movement,
        EnemyRotation rotation,
        Health health,
        IReadOnlyList<BodyPartBehaviour> bodyParts,
        EnemySettings settings,
        PlayerBehaviour player
    )
    {
        this.settings = settings;
        this.player = player;
        this.bodyParts = bodyParts;
        this.transform = transform;
        Movement = movement;
        Rotation = rotation;
        Health = health;
        Health.OnHealthChanged += HandleHealthChanged;
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.OnBodyPartCollisionEnter += HandleBodyPartCollisionEnter;
            bodyPart.OnBodyPartCollisionExit += HandleBodyPartCollisionExit;
            bodyPart.OnBodyPartCollisionStay += HandleBodyPartCollisionStay;
        }
    }

    public void Start ()
    {
        Health.Start();
        Movement.Start();
    }

    public void FixedUpdate ()
    {
        Movement.FixedUpdate();
        Rotation.FixedUpdate();

        if (IsCloseToPlayer())
            OnCloseToPlayer?.Invoke(this);
    }

    public void HandleBodyPartCollisionEnter (BodyPart part, Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out var _))
            HandlePlayerCollision();

        if (collider.TryGetComponent<ProjectileBehaviour>(out var projectile))
            HandleProjectileCollision(part, projectile);
    }

    public void HandleBodyPartCollisionStay (BodyPart part, Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out var _))
            HandlePlayerCollision();
    }

    public void HandleBodyPartCollisionExit (BodyPart part, Collider collider)
    {
        if (collider.TryGetComponent<PlayerBehaviour>(out var _))
            Movement.Resume();
    }

    void HandlePlayerCollision ()
    {
        if (CanAttack)
            nextAttackTime = Time.time + settings.AttackInterval;
        Movement.Stop();
    }

    void HandleProjectileCollision (BodyPart part, ProjectileBehaviour projectile)
    {
        if (Health.IsDead)
            return;

        var damageMultiplier = settings.HealthSettings.BodyPartDamageSettings.GetMultiplier(part);
        float damage = projectile.Projectile.Damage * damageMultiplier;
        Health.TakeDamage(damage);
        OnHit?.Invoke(this, part);
    }

    void HandleHealthChanged (float previous, float current)
    {
        if (current <= 0)
            HandleDeath();
    }

    void HandleDeath ()
    {
        Movement.Enabled = false;
        Rotation.Enabled = false;
        foreach (var bodyPart in bodyParts)
            bodyPart.Enabled = false;
        OnDeath?.Invoke(this);
    }

    bool IsCloseToPlayer () => GetDistanceToPlayer() <= PLAYER_DISTANCE_THRESHOLD_SQUARED;

    float GetDistanceToPlayer () => Vector3.SqrMagnitude(
        player.transform.position - transform.position
    );
}