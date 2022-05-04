using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameSettings GameSettings { get; private set; }
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public WeaponBehaviour WeaponBehaviour { get; private set; }

    void Awake ()
    {
        WeaponBehaviour = GetComponentInChildren<WeaponBehaviour>();

        Health = new Health();
        PlayerMovement = new PlayerMovement(transform, MovementSettings, InputManager.Instance);
        PlayerRotation = new PlayerRotation(transform, GameSettings, InputManager.Instance);
        PlayerAction = new PlayerAction(InputManager.Instance, WeaponBehaviour.Weapon);
    }

    void Update ()
    {
        PlayerMovement.Update();
        PlayerRotation.Update();
        PlayerAction.Update();
    }
}
