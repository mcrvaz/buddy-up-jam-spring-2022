using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    public bool Enabled
    {
        get => enabled;
        set => SetEnabled(value);
    }

    readonly Transform transform;
    readonly MovementSettings settings;
    readonly PlayerBehaviour player;
    readonly NavMeshAgent agent;

    bool enabled = true;

    public EnemyMovement (
        Transform transform,
        MovementSettings settings,
        PlayerBehaviour player,
        NavMeshAgent agent
    )
    {
        this.transform = transform;
        this.settings = settings;
        this.player = player;
        this.agent = agent;
    }

    public void Start ()
    {
        SetupAgent();
    }

    public void FixedUpdate ()
    {
        if (!Enabled)
            return;
        agent.destination = player.Rigidbody.position;
    }

    public void Stop ()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    void SetupAgent ()
    {
        agent.speed = settings.ForwardSpeed;
        agent.acceleration = settings.Acceleration;
    }

    void SetEnabled (bool enabled)
    {
        this.enabled = enabled;
        if (!enabled)
            Stop();
    }
}