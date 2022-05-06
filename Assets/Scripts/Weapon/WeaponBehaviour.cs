using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [field: SerializeField] public WeaponSettings WeaponSettings { get; private set; }
    [field: SerializeField] public Transform[] ProjectileSpawnPoints { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }

    public Weapon Weapon { get; private set; }
    public WeaponAnimation WeaponAnimation { get; private set; }

    void Awake ()
    {
        Weapon = new Weapon(transform, ProjectileSpawnPoints, WeaponSettings, this);
        WeaponAnimation = new WeaponAnimation(Weapon, Animation);
    }

    void Start ()
    {
    }
}