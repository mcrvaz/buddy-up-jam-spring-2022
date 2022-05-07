using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    public virtual bool Enabled
    {
        get => enabled;
        set => enabled = value;
    }

    public bool IsGrounded
    {
        get => grounded;
        set
        {
            grounded = value;
            if (grounded)
                jumpCount = 0;
        }
    }

    readonly MovementSettings settings;
    readonly Transform transform;
    readonly InputManager inputManager;
    readonly Rigidbody rb;

    bool enabled = true;
    int jumpQueue;
    int jumpCount;
    bool grounded;
    Vector3 lastInputDirection;

    public PlayerMovement (
        Transform transform,
        MovementSettings settings,
        InputManager inputManager,
        Rigidbody rb
    )
    {
        this.settings = settings;
        this.transform = transform;
        this.inputManager = inputManager;
        this.rb = rb;
    }

    public void Start () { }

    public void Update ()
    {
        if (!Enabled)
            return;
        lastInputDirection = GetNormalizedInputDirection();
        if (inputManager.GetJumpDown())
            jumpQueue++;
    }

    public void FixedUpdate ()
    {
        if (!Enabled)
            return;
        ApplyInput(lastInputDirection);
        TryJump();
    }

    public void Stop ()
    {
        rb.velocity = Vector3.zero;
    }

    Vector3 GetNormalizedInputDirection () =>
        new Vector3(inputManager.GetHorizontal(), 0, inputManager.GetVertical()).normalized;

    void ApplyInput (in Vector3 direction)
    {
        float dot = Vector3.Dot(transform.forward, direction);
        float maxSpeed = dot >= 0 ? settings.ForwardSpeed : settings.BackwardSpeed;
        float sqrSpeed = maxSpeed * maxSpeed;
        Vector3 xMov = transform.forward * direction.z;
        Vector3 zMov = transform.right * direction.x;
        Vector3 newDirection = xMov + zMov;
        newDirection.y = 0;

        rb.velocity += newDirection * settings.Acceleration;
        if (rb.velocity.sqrMagnitude > sqrSpeed)
            rb.velocity *= 0.99f;
    }

    void TryJump ()
    {
        if (jumpQueue == 0)
            return;
        jumpQueue--;

        if (jumpCount >= settings.MaxJumpCount)
            return;
        jumpCount++;
        rb.AddForce(transform.up * settings.JumpForce);
    }
}