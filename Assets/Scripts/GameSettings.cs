using UnityEngine;

public class GameSettings : ScriptableObject
{
    [field: SerializeField] public int MaxFPS { get; private set; } = 144;
    [field: SerializeField] public bool VSync { get; private set; } = false;
    [field: SerializeField] public FullScreenMode Fullscreen { get; private set; } = FullScreenMode.MaximizedWindow;
}