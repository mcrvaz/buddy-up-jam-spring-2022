using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameSettings GameSettings { get; private set; }
    [field: SerializeField] public PlayerSettings Settings { get; private set; }
    [field: SerializeField] public CameraBehaviour MainCamera { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public AudioSource PlayerAudioSource { get; private set; }
    [field: SerializeField] public AudioSource PlayerFootstepsAudioSource { get; private set; }

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerWeapon PlayerWeapon { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public IReadOnlyList<IWeaponBehaviour> WeaponBehaviours { get; private set; }
    public PlayerWeapon Weapon { get; private set; }
    public Player Player { get; private set; }
    public Currency Currency { get; private set; }
    public PlayerSounds PlayerSounds { get; private set; }

    GameAudioBehaviour audioBehaviour;

    void Awake ()
    {
        audioBehaviour = FindObjectOfType<GameAudioBehaviour>();
        WeaponBehaviours = GetComponentsInChildren<IWeaponBehaviour>();
        Health = new Health(Settings.HealthSettings);
        PlayerMovement = new PlayerMovement(
            transform,
            Settings.MovementSettings,
            InputManager.Instance,
            Rigidbody
        );
        PlayerRotation = new PlayerRotation(Rigidbody, GameSettings, InputManager.Instance);
        PlayerWeapon = new PlayerWeapon(WeaponBehaviours, Settings);
        PlayerAction = new PlayerAction(InputManager.Instance, PlayerWeapon);
        Player = new Player(
            PlayerMovement,
            PlayerRotation,
            PlayerAction,
            PlayerWeapon,
            Health,
            Collider,
            MainCamera.CameraShake
        );
    }

    void Start ()
    {
        PlayerSounds = new PlayerSounds(
            Player,
            audioBehaviour.AudioManager,
            PlayerAudioSource,
            PlayerFootstepsAudioSource
        );

        Player.Start();
    }

    void FixedUpdate ()
    {
        PlayerSounds.Update();
        Player.FixedUpdate();
    }

    void Update ()
    {
        Player.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        Player.HandleCollisionEnter(collider);
    }

    void OnCollisionEnter (Collision collisionInfo)
    {
        Player.HandleCollisionEnter(collisionInfo);
    }

    void OnCollisionStay (Collision collisionInfo)
    {
        Player.HandleCollisionStay(collisionInfo);
    }

    void OnCollisionExit (Collision collisionInfo)
    {
        Player.HandleCollisionExit(collisionInfo);
    }
}
