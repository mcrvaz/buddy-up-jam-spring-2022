using UnityEngine;

public abstract class Movement
{
    public bool Enabled { get; set; } = true;

    protected Vector3 Gravity => Physics.gravity * settings.GravityModifier;

    protected readonly MovementSettings settings;
    protected readonly Transform transform;
    protected readonly int groundLayerMask;
    protected readonly float entityHeight;

    protected int jumpCount;
    protected float jumpEndTime = float.MinValue;
    protected Vector3 velocity;

    public Movement (Transform transform, MovementSettings settings)
    {
        this.settings = settings;
        this.transform = transform;

        groundLayerMask = LayerMask.GetMask("Ground");
        entityHeight = GetDistanceToGround(transform.position);
    }

    public abstract void Update ();

    public void Stop ()
    {
        velocity = Vector3.zero;
    }

    protected bool IsAerial (in Vector3 position) => GetDistanceToGround(position) > entityHeight;

    protected float GetDistanceToGround (in Vector3 position)
    {
        Physics.Raycast(position, Vector3.down, out var hit, Mathf.Infinity, groundLayerMask);
        return hit.distance;
    }

    protected void Jump (in float time)
    {
        if (jumpCount >= settings.MaxJumpCount)
            return;

        jumpCount++;
        jumpEndTime = time + settings.JumpDuration;
    }

    protected void ApplyJump (ref Vector3 position, in float deltaTime, in float time)
    {
        if (time >= jumpEndTime)
            return;

        velocity += -Gravity * settings.JumpSpeed * deltaTime;
        position += velocity * deltaTime;
    }

    protected void ApplyGravity (ref Vector3 position, in float deltaTime)
    {
        velocity += Gravity * deltaTime;
        position += velocity * deltaTime;
        position.y = Mathf.Max(position.y, entityHeight);
    }

    protected void TryLand (ref Vector3 position)
    {
        if (!IsAerial(position))
        {
            jumpCount--;
            jumpCount = Mathf.Max(jumpCount, 0);
            velocity = Vector3.zero;
        }
    }
}