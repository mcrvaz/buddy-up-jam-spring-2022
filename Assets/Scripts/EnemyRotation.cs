using UnityEngine;

public class EnemyRotation
{
    readonly Transform transform;
    readonly PlayerBehaviour player;

    public EnemyRotation (Transform transform, PlayerBehaviour player)
    {
        this.transform = transform;
        this.player = player;
    }

    public void Update ()
    {
        transform.LookAt(player.transform);
    }
}