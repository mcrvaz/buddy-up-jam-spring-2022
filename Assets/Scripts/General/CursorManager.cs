using UnityEngine;

public static class CursorManager
{
    public static void Toggle ()
    {
        SetVisible(!Cursor.visible);
    }

    public static void SetVisible (bool visible)
    {
        if (visible)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}