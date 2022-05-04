using UnityEngine;
using UnityEngine.Pool;

public class ProjectileBehaviour : MonoBehaviour
{
    [field: SerializeField] public ProjectileSettings ProjectileSettings { get; private set; }

    public ProjectilePool Pool { get; set; }
    public Projectile Projectile { get; private set; }
    public Vector3 Forward
    {
        get => transform.forward;
        set => transform.forward = value;
    }

    void Awake ()
    {
        Projectile = new Projectile(transform, ProjectileSettings);
        Projectile.OnDestroyed += HandleDestroy;
    }

    void OnEnable ()
    {
        Projectile.Reset();
    }

    void Update ()
    {
        Projectile.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        Projectile.HandleCollision(collider);
    }

    void HandleDestroy ()
    {
        Pool.Release(this);
    }
}