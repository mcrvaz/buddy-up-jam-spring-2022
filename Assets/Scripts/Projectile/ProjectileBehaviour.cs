using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [field: SerializeField] public ProjectileSettings ProjectileSettings { get; private set; }

    public ProjectilePool Pool { get; set; }
    public Projectile Projectile { get; private set; }

    public float Damage
    {
        get => Projectile.Damage;
        set => Projectile.Damage = value;
    }
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

    void Start ()
    {
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