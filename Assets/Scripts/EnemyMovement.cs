using UnityEngine;

public class EnemyMovement : Movement
{
    readonly PlayerBehaviour player;

    public EnemyMovement (Transform transform, MovementSettings settings, PlayerBehaviour player)
        : base(transform, settings)
    {
        this.player = player;
    }

    public override void Update ()
    {
        Vector3 position = transform.position;
        float deltaTime = Time.deltaTime;
        Move(ref position, GetPlayerDirection(), deltaTime);
        ApplyGravity(ref position, deltaTime);
        TryLand(ref position);
        transform.position = position;
    }

    void Move (ref Vector3 position, in Vector3 direction, in float deltaTime)
    {
        position += direction * settings.ForwardSpeed * deltaTime;
    }

    Vector3 GetPlayerDirection () => player.transform.position - transform.position;
}