using UnityEngine;

public class PlayerMovement : Movement
{
    readonly InputManager inputManager;

    public PlayerMovement (Transform transform, MovementSettings settings, InputManager inputManager)
        : base(transform, settings)
    {
        this.inputManager = inputManager;
    }

    public override void Update ()
    {
        if (!Enabled)
            return;

        Vector3 position = transform.position;
        float deltaTime = Time.deltaTime;
        float time = Time.time;
        if (inputManager.GetJumpDown())
            Jump(time);

        Move(ref position, GetNormalizedInputDirection(), deltaTime);
        ApplyJump(ref position, deltaTime, time);
        ApplyGravity(ref position, deltaTime);
        TryLand(ref position);
        transform.position = position;
    }

    Vector3 GetNormalizedInputDirection () => new Vector3(inputManager.GetHorizontal(), 0, inputManager.GetVertical()).normalized;

    void Move (ref Vector3 position, in Vector3 direction, in float deltaTime)
    {
        float dot = Vector3.Dot(transform.forward, direction);
        float speed = dot >= 0 ? settings.ForwardSpeed : settings.BackwardSpeed;

        Vector3 xMov = transform.forward * direction.z;
        Vector3 zMov = transform.right * direction.x;
        xMov.y = 0;
        zMov.y = 0;

        position += (xMov + zMov) * speed * deltaTime;
    }
}