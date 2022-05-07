using UnityEngine;

public class PlayerMovement
{
    public virtual bool Enabled
    {
        get => enabled;
        set => SetEnabled(value);
    }

    readonly MovementSettings settings;
    readonly Transform transform;
    readonly int groundLayerMask;
    readonly float entityHeight;
    readonly InputManager inputManager;
    readonly Rigidbody rb;

    int jumpCount;
    bool enabled = true;
    bool jumped;
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
        groundLayerMask = LayerMask.GetMask("Ground");
        entityHeight = GetDistanceToGround(transform.position);
    }

    public void Start () { }

    public void Update ()
    {
        if (!Enabled)
            return;
        lastInputDirection = GetNormalizedInputDirection();
        if (inputManager.GetJumpDown())
            jumped = true;
    }

    public void FixedUpdate ()
    {
        if (!Enabled)
            return;
        ApplyInput(lastInputDirection);
        TryJump();
        TryLand();
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
        if (!jumped)
            return;

        if (jumpCount >= settings.MaxJumpCount)
            return;

        jumpCount++;
        UnityEngine.Debug.Log("Jump");
        rb.AddForce(transform.up * settings.JumpForce);
        jumped = false;
    }

    void TryLand ()
    {
        if (IsAerial(rb.position))
            return;
        jumpCount = 0;
    }

    bool IsAerial (in Vector3 position) => GetDistanceToGround(position) > entityHeight;

    float GetDistanceToGround (in Vector3 position)
    {
        Physics.Raycast(position, Vector3.down, out var hit, Mathf.Infinity, groundLayerMask);
        return hit.distance;
    }

    void SetEnabled (bool enabled) => this.enabled = enabled;
}