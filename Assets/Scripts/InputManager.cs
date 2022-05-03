using UnityEngine;

public class InputManager
{
    public static InputManager Instance => instance ??= new InputManager();
    static InputManager instance;

    public float GetHorizontal () => Input.GetAxis("Horizontal");
    public float GetVertical () => Input.GetAxis("Vertical");
    public bool GetFireDown () => Input.GetButtonDown("Fire");
    public bool GetAltFireDown () => Input.GetButtonDown("AltFire");
    public bool GetJumpDown () => Input.GetButtonDown("Jump");
}