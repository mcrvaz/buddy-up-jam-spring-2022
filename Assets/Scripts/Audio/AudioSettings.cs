using UnityEngine;

public class AudioSettings : ScriptableObject
{
    [field: SerializeField, Range(0f, 1f)] public float MasterVolume { get; set; } = 1f;
    [field: SerializeField, Range(0f, 1f)] public float MusicVolume { get; set; } = 1f;
    [field: SerializeField, Range(0f, 1f)] public float SoundEffectsVolume { get; set; } = 1f;
}
