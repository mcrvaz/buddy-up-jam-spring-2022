public class PlayerAction
{
    public bool Enabled { get; set; } = true;

    readonly InputManager inputManager;
    readonly PlayerWeapon playerWeapon;

    public PlayerAction (InputManager inputManager, PlayerWeapon playerWeapon)
    {
        this.inputManager = inputManager;
        this.playerWeapon = playerWeapon;
    }

    public void Update ()
    {
        if (!Enabled)
            return;

        var activeWeapon = playerWeapon.ActiveWeapon;
        activeWeapon.Update();
        if (inputManager.GetFireDown())
            activeWeapon.Shoot();
        else if (inputManager.GetReloadDown())
            activeWeapon.Reload();

        int numericKeyPressed = inputManager.GetNumericKeyPressed();
        if (numericKeyPressed != -1)
            playerWeapon.ChangeWeapon(numericKeyPressed);
    }
}