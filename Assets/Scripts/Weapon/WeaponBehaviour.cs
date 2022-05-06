using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [field: SerializeField] public WeaponSettings WeaponSettings { get; private set; }
    [field: SerializeField] public Transform[] ProjectileSpawnPoints { get; private set; }

    public Weapon Weapon { get; private set; }

    void Awake ()
    {
        Weapon = new Weapon(transform, ProjectileSpawnPoints, WeaponSettings);
    }

    void Start ()
    {
    }
}