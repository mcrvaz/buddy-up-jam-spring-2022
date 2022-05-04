using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    [field: SerializeField] public Camera WeaponCamera { get; private set; }

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public Health Health { get; private set; }
    public Weapon Weapon { get; private set; }

    void Awake ()
    {
        PlayerMovement = new PlayerMovement(transform, MovementSettings, InputManager.Instance);
        PlayerRotation = new PlayerRotation(transform, MovementSettings, InputManager.Instance);
        PlayerAction = new PlayerAction();
        Health = new Health();
        Weapon = new Weapon();
    }

    void Update ()
    {
        PlayerMovement.Update();
        PlayerRotation.Update();
    }
}
