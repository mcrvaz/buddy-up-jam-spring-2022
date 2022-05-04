using System;
using UnityEngine;

public class Projectile
{
    public event Action OnDestroyed;

    readonly Transform transform;
    readonly ProjectileSettings settings;

    float destroyTime;
    bool destroyed;

    public Projectile (Transform transform, ProjectileSettings settings)
    {
        this.transform = transform;
        this.settings = settings;
    }

    public void Reset ()
    {
        destroyTime = Time.time + settings.TimeToLive;
        destroyed = false;
    }

    public void HandleCollision (Collider collider)
    {
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
