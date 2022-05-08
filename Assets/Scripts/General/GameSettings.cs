using System;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    public event Action OnUpdated;

    [field: SerializeField] public int MaxFPS { get; set; } = 144;
    [field: SerializeField] public bool VSync { get; set; } = false;
    [field: SerializeField] public FullScreenMode Fullscreen { get; set; } = FullScreenMode.MaximizedWindow;
    [field: SerializeField] public float MouseSensitivity { get; set; } = 1f;
    [field: SerializeField] public AudioSettings Audio { get; set; }

    public void RaiseOnUpdated () => OnUpdated?.Invoke();
}