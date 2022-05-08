using UnityEngine;

public abstract class WeaponBehaviour<T> : MonoBehaviour, IWeaponBehaviour where T : Weapon
{
    [field: SerializeField] public Transform[] ProjectileSpawnPoints { get; private set; }
    [field: SerializeField] public Transform WeaponTransform { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }

    public Weapon Weapon { get; private set; }
    public WeaponAnimation WeaponAnimation { get; private set; }

    protected PlayerBehaviour playerBehaviour;

    void Awake ()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        Weapon = CreateWeapon();
        WeaponAnimation = new WeaponAnimation(Weapon, Animation);
    }

    void Start ()
    {

    }

    protected abstract T CreateWeapon ();
}