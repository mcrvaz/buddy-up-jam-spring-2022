public class PlayerAction
{
    public bool Enabled { get; set; } = true;

    readonly InputManager inputManager;
    readonly Weapon weapon;

    public PlayerAction (InputManager inputManager, Weapon weapon)
    {
        this.inputManager = inputManager;
        this.weapon = weapon;
    }

    public void Update ()
    {
        if (!Enabled)
            return;
        weapon.Update();
        if (inputManager.GetFireDown())
            weapon.Shoot();
    }
}