using System;
using UnityEngine;

public class Player
{
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public Weapon Weapon { get; private set; }

    public Player (
        PlayerMovement playerMovement,
        PlayerRotation playerRotation,
        PlayerAction playerAction,
        Health health,
        Weapon weapon
    )
    {
        PlayerMovement = playerMovement;
        PlayerRotation = playerRotation;
        PlayerAction = playerAction;
        Health = health;
        Weapon = weapon;
    }

    public void Start ()
    {
        Health.Start();
    }

    public void Update ()
    {
        PlayerMovement.Update();
        PlayerRotation.Update();
        PlayerAction.Update();
    }

    public void HandleCollision (Collider collider)
    {
        if (collider.TryGetComponent<EnemyBehaviour>(out var enemy))
            HandleEnemyCollision(enemy);
    }

    void HandleEnemyCollision (EnemyBehaviour enemy)
    {
        throw new NotImplementedException();
    }
}