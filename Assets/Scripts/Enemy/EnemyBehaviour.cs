using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public event Action<EnemyBehaviour> OnDeath;

    [field: SerializeField] public EnemySettings Settings { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }
    public Enemy Enemy { get; private set; }

    PlayerBehaviour player;
    BodyPartBehaviour[] bodyParts;

    void Awake ()
    {
        bodyParts = GetComponentsInChildren<BodyPartBehaviour>();
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        Movement = new EnemyMovement(transform, Settings.MovementSettings, player, Agent);
        Rotation = new EnemyRotation(transform, player);
        Health = new Health(Settings.HealthSettings);
        Enemy = new Enemy(Movement, Rotation, Health, bodyParts, Settings);

        Enemy.OnDeath += HandleDeath;

        Enemy.Start();
    }

    void FixedUpdate ()
    {
        Enemy.FixedUpdate();
    }

    void HandleDeath ()
    {
        OnDeath?.Invoke(this);
    }
}
