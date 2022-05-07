using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [field: SerializeField] public WeaponSettings WeaponSettings { get; private set; }
    [field: SerializeField] public Transform[] ProjectileSpawnPoints { get; private set; }
    [field: SerializeField] public Transform WeaponTransform { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }

    public Weapon Weapon { get; private set; }
    public WeaponAnimation WeaponAnimation { get; private set; }

    void Awake ()
    {
        Weapon = new Weapon(WeaponTransform, ProjectileSpawnPoints, WeaponSettings, this, WeaponCamera);
        WeaponAnimation = new WeaponAnimation(Weapon, Animation);
    }

    void Start ()
    {
    }
}