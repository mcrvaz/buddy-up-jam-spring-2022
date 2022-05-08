using UnityEngine;

public class AudioClipDatabaseSettings : ScriptableObject
{
    [field: SerializeField] public AudioClip TestClip { get; private set; }
}
