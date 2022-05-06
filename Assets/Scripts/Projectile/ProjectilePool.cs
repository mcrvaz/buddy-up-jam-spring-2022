using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool
{
    public ObjectPool<ProjectileBehaviour> Pool { get; }

    readonly ProjectileBehaviour prefab;

    public ProjectilePool (ProjectileBehaviour prefab)
    {
        this.prefab = prefab;
        Pool = new ObjectPool<ProjectileBehaviour>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool
        );
    }

    public ProjectileBehaviour Get () => Pool.Get();

    public void Release (ProjectileBehaviour obj) =>
        Pool.Release(obj);

    ProjectileBehaviour CreatePooledItem ()
    {
        var projectile = GameObject.Instantiate(prefab);
        projectile.Pool = this;
        return projectile;
    }

    void OnReturnedToPool (ProjectileBehaviour projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    void OnTakeFromPool (ProjectileBehaviour projectile)
    {
        projectile.gameObject.SetActive(true);
    }
}