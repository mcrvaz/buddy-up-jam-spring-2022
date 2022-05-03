using UnityEngine;

public class PlayerMovement
{
    Vector3 Gravity => Physics.gravity * settings.GravityModifier;

    readonly MovementSettings settings;
    readonly Transform transform;
    readonly InputManager inputManager;
    readonly int groundLayerMask;
    readonly float playerHeight;

    int jumpCount;
    float jumpEndTime = float.MinValue;
    Vector3 velocity;

    public PlayerMovement (Transform transform, MovementSettings settings, InputManager inputManager)
    {
        this.settings = settings;
        this.transform = transform;
        this.inputManager = inputManager;

        groundLayerMask = LayerMask.GetMask("Ground");
        playerHeight = GetDistanceToGround(transform.position);
    }

    public void Update ()
    {
        Vector3 position = transform.position;
        float deltaTime = Time.deltaTime;
        float time = Time.time;
        if (inputManager.GetJumpDown())
            Jump(time);

        Move(ref position, GetMovementDirection(), deltaTime);
        ApplyJump(ref position, deltaTime, time);
        ApplyGravity(ref position, deltaTime);
        TryLand(ref position);
        transform.position = position;
    }

    bool IsAerial (in Vector3 position) => GetDistanceToGround(position) > playerHeight;

    Vector3 GetMovementDirection ()
    {
        var input = new Vector2(inputManager.GetHorizontal(), inputManager.GetVertical());
        var clamped = Vector2.ClampMagnitude(input, 1f);
        return new Vector3(clamped.x, 0, clamped.y);
    }

    float GetDistanceToGround (in Vector3 position)
    {
        Physics.Raycast(position, Vector3.down, out var hit, Mathf.Infinity, groundLayerMask);
        return hit.distance;
    }

    void Move (ref Vector3 position, in Vector3 direction, in float deltaTime)
    {
        var dot = Vector3.Dot(transform.forward, direction);
        var speed = dot >= 0 ? settings.ForwardSpeed : settings.BackwardSpeed;
        position += direction * speed * deltaTime;
    }

    void Jump (in float time)
    {
        if (jumpCount >= settings.MaxJumpCount)
            return;

        jumpCount++;
        jumpEndTime = time + settings.JumpDuration;
    }

    void ApplyJump (ref Vector3 position, in float deltaTime, in float time)
    {
        if (time >= jumpEndTime)
            return;

        velocity += -Gravity * settings.JumpSpeed * deltaTime;
        position += velocity * deltaTime;
    }

    void ApplyGravity (ref Vector3 position, in float deltaTime)
    {
        velocity += Gravity * deltaTime;
        position += velocity * deltaTime;
        position.y = Mathf.Max(position.y, playerHeight);
    }

    void TryLand (ref Vector3 position)
    {
        if (!IsAerial(position))
        {
            jumpCount--;
            jumpCount = Mathf.Max(jumpCount, 0);
            velocity = Vector3.zero;
        }
    }
}