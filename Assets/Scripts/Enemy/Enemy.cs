using System;
using UnityEngine;

public class Enemy
{
    public event Action OnDeath;

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }
    public Collider Collider { get; private set; }

    public Enemy (EnemyMovement movement, EnemyRotation rotation, Health health, Collider collider)
    {
        Movement = movement;
        Rotation = rotation;
        Health = health;
        Collider = collider;
        Health.OnHealthChanged += HandleHealthChanged;
    }

    public void Start ()
    {
        Health.Start();
    }

    public void Update ()
    {
        Movement.Update();
        Rotation.Update();
    }

    public void HandleCollision (Collider collider)
    {
        UnityEngine.Debug.Log("Enemy collided with " + collider.name);
        if (collider.TryGetComponent<ProjectileBehaviour>(out var projectile))
            HandleProjectileCollision(projectile);
    }

    void HandleProjectileCollision (ProjectileBehaviour projectile)
    {
        UnityEngine.Debug.Log(Health.Current);
        if (Health.IsDead)
            return;

        Health.TakeDamage(projectile.Projectile.Damage);
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
        Collider.enabled = false;
        OnDeath?.Invoke();
    }
}