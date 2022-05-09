using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    public bool Enabled
    {
        get => enabled;
        set => SetEnabled(value);
    }

    readonly MonoBehaviour coroutineRunner;
    readonly MovementSettings settings;
    readonly PlayerBehaviour player;
    readonly NavMeshAgent agent;

    bool enabled = true;
    float movementResumeTime;
    Coroutine movementResumeRoutine;

    public EnemyMovement (
        MonoBehaviour coroutineRunner,
        MovementSettings settings,
        PlayerBehaviour player,
        NavMeshAgent agent
    )
    {
        this.coroutineRunner = coroutineRunner;
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

    public void Resume ()
    {
        agent.isStopped = false;
    }

    public void Stop ()
    {
        if (movementResumeRoutine != null)
            coroutineRunner.StopCoroutine(movementResumeRoutine);

        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    public void Stop (float seconds)
    {
        Stop();
        movementResumeTime = Time.time + seconds;
        movementResumeRoutine = coroutineRunner.StartCoroutine(StopRoutine());
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

    IEnumerator StopRoutine ()
    {
        while (Time.time < movementResumeTime)
            yield return null;
        Resume();
    }
}