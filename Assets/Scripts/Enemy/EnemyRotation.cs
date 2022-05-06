using UnityEngine;

public class EnemyRotation
{
    public bool Enabled { get; set; } = true;

    readonly Transform transform;
    readonly PlayerBehaviour player;

    public EnemyRotation (Transform transform, PlayerBehaviour player)
    {
        this.transform = transform;
        this.player = player;
    }

    public void Update ()
    {
        if (!Enabled)
            return;

        transform.LookAt(player.transform);
    }
}