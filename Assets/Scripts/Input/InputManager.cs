using UnityEngine;

public class InputManager
{
    public static InputManager Instance => instance ??= new InputManager();
    static InputManager instance;

    readonly KeyCode[] numericKeyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };

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
    public bool GetReloadDown () => Input.GetButtonDown("Reload");
    public bool GetAnyKeyDown () => Input.anyKey;
    public int GetNumericKeyPressed ()
    {
        for (int i = 0; i < numericKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(numericKeyCodes[i]))
                return i + 1;
        }
        return -1;
    }
}