using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    readonly PlayerBehaviour player;
    readonly NavMeshAgent agent;

    public EnemyMovement (
        Transform transform,
        MovementSettings settings,
        PlayerBehaviour player,
        NavMeshAgent agent
    )
        : base(transform, settings)
    {
        this.player = player;
        this.agent = agent;
    }

    public override void Start ()
    {
        SetupAgent();
    }

    public override void Update ()
    {
        if (!Enabled)
            return;
        agent.destination = player.transform.position;
    }

    public override void Stop ()
    {
        base.Stop();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    void SetupAgent ()
    {
        agent.speed = settings.ForwardSpeed;
        agent.acceleration = settings.Acceleration;
    }

    protected override void SetEnabled (bool enabled)
    {
        base.SetEnabled(enabled);
        if (!enabled)
            Stop();
    }
}