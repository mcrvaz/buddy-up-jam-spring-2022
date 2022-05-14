using System;
using UnityEngine;

public class Player
{
    public event Action OnHit;
    public event Action OnDeath;

    public PlayerMovement Movement { get; private set; }
    public PlayerRotation Rotation { get; private set; }
    public PlayerAction Action { get; private set; }
    public Health Health { get; private set; }
    public Collider Collider { get; private set; }
    public IWeapon ActiveWeapon => playerWeapon.ActiveWeapon;

    readonly int groundLayer;
    readonly PlayerWeapon playerWeapon;
    readonly CameraShake cameraShake;

    public Player (
        PlayerMovement playerMovement,
        PlayerRotation playerRotation,
        PlayerAction playerAction,
        PlayerWeapon playerWeapon,
        Health health,
        Collider collider,
        CameraShake cameraShake
    )
    {
        this.cameraShake = cameraShake;
        this.playerWeapon = playerWeapon;
        Movement = playerMovement;
        Rotation = playerRotation;
        Action = playerAction;
        Health = health;
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

    public void Teleport (Transform targetTransform)
    {
        Movement.Teleport(targetTransform);
    }

    public void HandleCollisionEnter (Collision collisionInfo) =>
        HandleCollisionEnter(collisionInfo.collider);

    public void HandleCollisionEnter (Collider collider)
    {
        if (collider.TryGetComponent<EnemyBodyPartBehaviour>(out var enemyBodyPart))
            HandleEnemyCollision(enemyBodyPart.EnemyBehaviour);

        if (collider.gameObject.layer == groundLayer)
            Movement.IsGrounded = true;
    }

    public void HandleCollisionStay (Collision collisionInfo) =>
        HandleCollisionStay(collisionInfo.collider);
    public void HandleCollisionStay (Collider collider)
    {
        if (collider.TryGetComponent<EnemyBodyPartBehaviour>(out var enemyBodyPart))
            HandleEnemyCollision(enemyBodyPart.EnemyBehaviour);
    }

    public void HandleCollisionExit (Collision collisionInfo) =>
        HandleCollisionExit(collisionInfo.collider);
    public void HandleCollisionExit (Collider collider)
    {
        if (collider.gameObject.layer == groundLayer)
            Movement.IsGrounded = false;
    }

    public IWeapon GetWeapon (WeaponId id) => playerWeapon.GetWeapon(id);

    void HandleEnemyCollision (EnemyBehaviour enemy)
    {
        if (Health.IsDead)
            return;
        if (Health.TakeDamage(enemy.Settings.Damage))
        {
            OnHit?.Invoke();
            cameraShake.PlayHitShake();
        }
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