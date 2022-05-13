using UnityEngine;

public abstract class WeaponBehaviour<T> : MonoBehaviour, IWeaponBehaviour where T : Weapon
{
    [field: SerializeField] public Transform[] ProjectileSpawnPoints { get; private set; }
    [field: SerializeField] public Transform WeaponTransform { get; private set; }
    [field: SerializeField] public Animation Animation { get; private set; }
    [field: SerializeField] public WeaponAnimationMapping WeaponAnimationMapping { get; private set; }

    public Weapon Weapon { get; private set; }
    public WeaponAnimation WeaponAnimation { get; private set; }
    public WeaponSoundManager WeaponSounds { get; private set; }

    protected PlayerBehaviour playerBehaviour;
    protected GameAudioBehaviour audioBehaviour;

    void Awake ()
    {
        audioBehaviour = FindObjectOfType<GameAudioBehaviour>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        Weapon = CreateWeapon();
    }

    void Start ()
    {
        WeaponAnimation = new WeaponAnimation(
            Weapon,
            Animation,
            GetComponentsInChildren<ParticleSystem>(true),
            WeaponAnimationMapping
        );
        WeaponSounds = new WeaponSoundManager(
            Weapon,
            CreateWeaponSounds()
        );
        Weapon.Start();
    }

    protected abstract T CreateWeapon ();
    protected abstract IWeaponSounds CreateWeaponSounds ();

    public void SetActive (bool active) => gameObject.SetActive(active);
}
