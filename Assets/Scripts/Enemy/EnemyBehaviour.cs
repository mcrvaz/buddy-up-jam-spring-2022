using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public event Action<EnemyBehaviour> OnDeath;
    public event Action<EnemyBehaviour, BodyPart> OnHit;
    public event Action<EnemyBehaviour> OnCloseToPlayer;

    [field: SerializeField] public EnemySettings Settings { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }
    public Enemy Enemy { get; private set; }
    public EnemyAnimation EnemyAnimation { get; private set; }

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
        Enemy = new Enemy(transform, Movement, Rotation, Health, bodyParts, Settings, player);
        EnemyAnimation = new EnemyAnimation(Enemy, Animation);
        Enemy.OnDeath += RaiseOnDeath;
        Enemy.OnHit += RaiseOnHit;
        Enemy.OnCloseToPlayer += RaiseOnCloseToPlayer;
        Enemy.Start();
        EnemyAnimation.Start();
    }

    void RaiseOnHit (Enemy enemy, BodyPart bodyPart) => OnHit?.Invoke(this, bodyPart);

    void RaiseOnDeath (Enemy enemy) => OnDeath?.Invoke(this);

    void RaiseOnCloseToPlayer (Enemy enemy) => OnCloseToPlayer?.Invoke(this);

    void FixedUpdate ()
    {
        Enemy.FixedUpdate();
    }
}
