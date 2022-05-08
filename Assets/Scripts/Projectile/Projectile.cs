using System;
using UnityEngine;

public class Projectile
{
    public event Action OnDestroyed;

    public float Damage { get; set; }

    readonly Transform transform;
    readonly ProjectileSettings settings;

    float destroyTime;
    bool destroyed;
    int penetrationLeft;

    public Projectile (Transform transform, ProjectileSettings settings)
    {
        this.transform = transform;
        this.settings = settings;
        penetrationLeft = settings.PenetrationCount;
    }

    public void Reset ()
    {
        destroyTime = Time.time + settings.TimeToLive;
        destroyed = false;
    }

    public void HandleCollision (Collider collider)
    {
        penetrationLeft--;
        if (penetrationLeft == 0)
            DestroySelf();
    }

    public void Update ()
    {
        if (!destroyed && Time.time >= destroyTime)
        {
            DestroySelf();
            return;
        }

        MoveForward();
    }

    void MoveForward ()
    {
        transform.position += transform.forward * settings.Speed * Time.deltaTime;
    }

    void DestroySelf ()
    {
        if (destroyed)
            return;

        destroyed = true;
        OnDestroyed();
    }
}
