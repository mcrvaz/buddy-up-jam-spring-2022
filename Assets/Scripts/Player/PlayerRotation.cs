using UnityEngine;

public class PlayerRotation
{
    public bool Enabled { get; set; } = true;

    const float VERTICAL_ANGLE_LIMIT = 45f;

    readonly GameSettings settings;
    readonly Rigidbody rb;
    readonly InputManager inputManager;

    Vector2 rotation = Vector2.zero;

    public PlayerRotation (Rigidbody rb, GameSettings settings, InputManager inputManager)
    {
        this.rb = rb;
        this.settings = settings;
        this.inputManager = inputManager;
    }

    public void Update ()
    {
        if (!Enabled)
            return;

        Rotate();
    }

    void Rotate ()
    {
        rotation.x += inputManager.GetMouseHorizontal() * settings.MouseSensitivity;
        rotation.y += inputManager.GetMouseVertical() * settings.MouseSensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -VERTICAL_ANGLE_LIMIT, VERTICAL_ANGLE_LIMIT);
        var horizontalRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
        rb.rotation = horizontalRotation * verticalRotation;
    }
}