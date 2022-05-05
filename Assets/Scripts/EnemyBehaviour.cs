using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public event Action<EnemyBehaviour> OnDeath;

    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }

    public EnemyMovement Movement { get; private set; }
    public EnemyRotation Rotation { get; private set; }

    PlayerBehaviour player;

    void Awake ()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        Movement = new EnemyMovement(transform, MovementSettings, player);
        Rotation = new EnemyRotation(transform, player);
    }

    void Update ()
    {
        Movement.Update();
        Rotation.Update();
    }
}
