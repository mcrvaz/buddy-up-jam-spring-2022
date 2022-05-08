using UnityEngine;

public class InputManager
{
    public static InputManager Instance => instance ??= new InputManager();
    static InputManager instance;

    public float GetHorizontal () => Input.GetAxisRaw("Horizontal");
    public float GetVertical () => Input.GetAxisRaw("Vertical");
    public float GetMouseHorizontal () => Input.GetAxisRaw("Mouse X");
    public float GetMouseVertical () => Input.GetAxisRaw("Mouse Y");
    public bool GetFireDown () => Input.GetButtonDown("Fire");
    public bool GetAltFireDown () => Input.GetButtonDown("AltFire");
    public bool GetJumpDown () => Input.GetButtonDown("Jump");
    public Vector3 GetMousePosition () => Input.mousePosition;
    public bool GetCancelDown () => Input.GetButtonDown("Cancel");
    public bool GetConfirmDown () => Input.GetButtonDown("Confirm");
}