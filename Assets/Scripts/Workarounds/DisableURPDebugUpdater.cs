using UnityEngine;
using UnityEngine.Rendering;

public class DisableURPDebugUpdater : MonoBehaviour
{
    void Awake ()
    {
        DebugManager.instance.enableRuntimeUI = false;
    }
}