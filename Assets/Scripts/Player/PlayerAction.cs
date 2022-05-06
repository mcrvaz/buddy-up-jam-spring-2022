public class PlayerAction
{
    readonly InputManager inputManager;
    readonly Weapon weapon;

    public PlayerAction (InputManager inputManager, Weapon weapon)
    {
        this.inputManager = inputManager;
        this.weapon = weapon;
    }

    public void Update ()
    {
        if (inputManager.GetFireDown())
            weapon.Shoot();
    }
}