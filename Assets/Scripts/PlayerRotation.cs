using UnityEngine;

public class PlayerRotation
{
    readonly MovementSettings settings;
    readonly Transform transform;
    readonly InputManager inputManager;
    readonly Camera camera;
    float cameraVerticalAngle;

    public PlayerRotation (Transform transform, MovementSettings settings, InputManager inputManager, Camera camera)
    {
        this.transform = transform;
        this.settings = settings;
        this.inputManager = inputManager;
        this.camera = camera;
    }

    public void Update ()
    {
        Rotate();
    }

    void Rotate ()
    {
        RotateHorizontal();
        // RotateVertical();
    }

    void RotateHorizontal ()
    {
        transform.Rotate(
            new Vector3(0f, inputManager.GetMouseHorizontal() * settings.RotationSpeed, 0f),
            Space.Self
        );
    }

    void RotateVertical ()
    {
        cameraVerticalAngle += inputManager.GetMouseVertical() * settings.RotationSpeed;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        transform.localEulerAngles = new Vector3(-cameraVerticalAngle, 0, 0);
    }
}