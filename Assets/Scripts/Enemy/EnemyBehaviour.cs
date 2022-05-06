using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public event Action<EnemyBehaviour> OnDeath;

    [field: SerializeField] public EnemySettings Settings { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }
    public Health Health { get; private set; }
    public Enemy Enemy { get; private set; }

    PlayerBehaviour player;

    void Awake ()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        Movement = new EnemyMovement(transform, Settings.MovementSettings, player);
        Rotation = new EnemyRotation(transform, player);
        Health = new Health(Settings.HealthSettings);
        Enemy = new Enemy(Movement, Rotation, Health, Collider);

        Enemy.OnDeath += HandleDeath;

        Enemy.Start();
    }

    void Update ()
    {
        Enemy.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        Enemy.HandleCollision(collider);
    }

    void HandleDeath ()
    {
        OnDeath?.Invoke(this);
    }
}
