using UnityEngine;

public class PlayerRotation
{
    const float VERTICAL_ANGLE_LIMIT = 45f;

    readonly GameSettings settings;
    readonly Transform transform;
    readonly InputManager inputManager;

    Vector2 rotation = Vector2.zero;

    public PlayerRotation (Transform transform, GameSettings settings, InputManager inputManager)
    {
        this.transform = transform;
        this.settings = settings;
        this.inputManager = inputManager;
    }

    public void Update ()
    {
        Rotate();
    }

    void Rotate ()
    {
        rotation.x += inputManager.GetMouseHorizontal() * settings.MouseSensibility;
        rotation.y += inputManager.GetMouseVertical() * settings.MouseSensibility;
        rotation.y = Mathf.Clamp(rotation.y, -VERTICAL_ANGLE_LIMIT, VERTICAL_ANGLE_LIMIT);
        var horizontalRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.localRotation = horizontalRotation * verticalRotation;
    }
}