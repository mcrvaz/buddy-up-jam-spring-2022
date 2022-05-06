using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameSettings GameSettings { get; private set; }
    [field: SerializeField] public PlayerSettings Settings { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public WeaponBehaviour WeaponBehaviour { get; private set; }
    public Player Player { get; private set; }
    public Currency Currency { get; private set; }

    void Awake ()
    {
        WeaponBehaviour = GetComponentInChildren<WeaponBehaviour>();
        Health = new Health(Settings.HealthSettings);
        PlayerMovement = new PlayerMovement(transform, Settings.MovementSettings, InputManager.Instance);
        PlayerRotation = new PlayerRotation(transform, GameSettings, InputManager.Instance);
    }

    void Start ()
    {
        PlayerAction = new PlayerAction(InputManager.Instance, WeaponBehaviour.Weapon);
        Player = new Player(
            PlayerMovement,
            PlayerRotation,
            PlayerAction,
            Health,
            WeaponBehaviour.Weapon
        );

        Player.Start();
    }

    void Update ()
    {
        Player.Update();
    }

    void OnTriggerEnter (Collider collider)
    {
        Player.HandleCollision(collider);
    }
}
