using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public event Action<EnemyBehaviour> OnDeath;
    public event Action<EnemyBehaviour, BodyPart> OnHit;

    [field: SerializeField] public EnemySettings Settings { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public AudioSource AudioSource { get; private set; }

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }
    public Enemy Enemy { get; private set; }

    PlayerBehaviour player;
    EnemyBodyPartBehaviour[] bodyParts;

    void Awake ()
    {
        bodyParts = GetComponentsInChildren<EnemyBodyPartBehaviour>();
        foreach (var bodyPart in bodyParts)
            bodyPart.EnemyBehaviour = this;

        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        Movement = new EnemyMovement(transform, Settings.MovementSettings, player, Agent);
        Rotation = new EnemyRotation(transform, player);
        Health = new Health(Settings.HealthSettings);
        Enemy = new Enemy(Movement, Rotation, Health, bodyParts, Settings);
        Enemy.OnDeath += RaiseOnDeath;
        Enemy.OnHit += RaiseOnHit;
        Enemy.Start();
    }

    void RaiseOnHit (Enemy enemy, BodyPart bodyPart) => OnHit?.Invoke(this, bodyPart);

    void RaiseOnDeath (Enemy enemy) => OnDeath?.Invoke(this);

    void FixedUpdate ()
    {
        Enemy.FixedUpdate();
    }
}
