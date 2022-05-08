using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameSettings GameSettings { get; private set; }
    [field: SerializeField] public PlayerSettings Settings { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    [field:SerializeField] public AudioSource PlayerAudioSource { get; private set; }
    [field:SerializeField] public AudioSource PlayerFootstepsAudioSource { get; private set; }

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public IWeaponBehaviour WeaponBehaviour { get; private set; }
    public Player Player { get; private set; }
    public Currency Currency { get; private set; }

    void Awake ()
    {
        WeaponBehaviour = GetComponentInChildren<IWeaponBehaviour>();
        Health = new Health(Settings.HealthSettings);
        PlayerMovement = new PlayerMovement(
            transform,
            Settings.MovementSettings,
            InputManager.Instance,
            Rigidbody
        );
        PlayerRotation = new PlayerRotation(Rigidbody, GameSettings, InputManager.Instance);
        PlayerAction = new PlayerAction(InputManager.Instance, WeaponBehaviour.Weapon);
        Player = new Player(
            PlayerMovement,
            PlayerRotation,
            PlayerAction,
            Health,
            WeaponBehaviour.Weapon,
            Collider
        );
    }

    void Start ()
    {
        Player.Start();
    }

    void FixedUpdate ()
    {
        Player.FixedUpdate();
    }

    void Update ()
    {
        Player.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        Player.HandleCollision(collider);
    }

    void OnCollisionEnter (Collision collisionInfo)
    {
        Player.HandleCollisionEnter(collisionInfo.collider);
    }

    void OnCollisionExit (Collision collisionInfo)
    {
        Player.HandleCollisionExit(collisionInfo.collider);
    }
}
