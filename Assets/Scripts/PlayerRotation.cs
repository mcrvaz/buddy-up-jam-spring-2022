using UnityEngine;

public class PlayerRotation
{
    const float VERTICAL_ANGLE_LIMIT = 89f;

    readonly MovementSettings settings;
    readonly Transform transform;
    readonly InputManager inputManager;

    Vector2 rotation = Vector2.zero;

    public PlayerRotation (Transform transform, MovementSettings settings, InputManager inputManager)
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
        rotation.x += inputManager.GetMouseHorizontal() * settings.RotationSpeed;
        rotation.y += inputManager.GetMouseVertical() * settings.RotationSpeed;
        rotation.y = Mathf.Clamp(rotation.y, -VERTICAL_ANGLE_LIMIT, VERTICAL_ANGLE_LIMIT);
        var horizontalRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var verticalRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.localRotation = horizontalRotation * verticalRotation;
    }
}