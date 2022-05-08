using System;
using UnityEngine;

public class Player
{
    public event Action OnDeath;

    public PlayerMovement Movement { get; private set; }
    public PlayerRotation Rotation { get; private set; }
    public PlayerAction Action { get; private set; }
    public Health Health { get; private set; }
    public Weapon Weapon { get; private set; }
    public Collider Collider { get; private set; }

    readonly int groundLayer;

    public Player (
        PlayerMovement playerMovement,
        PlayerRotation playerRotation,
        PlayerAction playerAction,
        Health health,
        Weapon weapon,
        Collider collider
    )
    {
        Movement = playerMovement;
        Rotation = playerRotation;
        Action = playerAction;
        Health = health;
        Weapon = weapon;
        Health.OnHealthChanged += HandleHealthChanged;
        Collider = collider;

        groundLayer = LayerMask.NameToLayer("Ground");
    }

    public void Start ()
    {
        Health.Start();
        Movement.Start();
    }

    public void FixedUpdate ()
    {
        Rotation.Update();
        Movement.FixedUpdate();
    }

    public void Update ()
    {
        Movement.Update();
        Action.Update();
    }

    public void HandleCollision (Collider collider)
    {
        if (collider.TryGetComponent<EnemyBodyPartBehaviour>(out var enemyBodyPart))
            HandleEnemyCollision(enemyBodyPart.EnemyBehaviour);
    }

    public void HandleCollisionEnter (Collider collider)
    {
        if (collider.gameObject.layer == groundLayer)
            Movement.IsGrounded = true;
    }

    public void HandleCollisionExit (Collider collider)
    {
        if (collider.gameObject.layer == groundLayer)
            Movement.IsGrounded = false;
    }

    public Weapon GetWeaponById (WeaponId _)
    {
        return Weapon;
    }

    void HandleEnemyCollision (EnemyBehaviour enemy)
    {
        if (Health.IsDead)
            return;
        Health.TakeDamage(enemy.Settings.Damage);
    }

    void HandleHealthChanged (float previous, float current)
    {
        if (current <= 0)
            HandleDeath();
    }

    void HandleDeath ()
    {
        Movement.Stop();
        Action.Enabled = false;
        Movement.Enabled = false;
        Rotation.Enabled = false;
        Collider.enabled = false;
        OnDeath?.Invoke();
    }
}