using UnityEngine;
using UnityEngine.Audio;

public class GameAudioBehaviour : MonoBehaviour
{
    [field: SerializeField] public AudioSource MusicSource { get; private set; }
    [field: SerializeField] public AudioSource DefaultSoundEffectsSource { get; private set; }
    [field: SerializeField] public AudioClipDatabaseSettings AudioClipDatabase { get; private set; }
    [field: SerializeField] public AudioMixer AudioMixer { get; private set; }

    public AudioManager AudioManager { get; private set; }

    void Awake ()
    {
        AudioManager = new AudioManager(
            MusicSource,
            DefaultSoundEffectsSource,
            AudioClipDatabase,
            AudioMixer
        );
    }

    void Start ()
    {
    }

    public void ApplySettings (GameSettings settings)
    {
        var audioSettings = settings.Audio;
        AudioManager.SetMasterVolume(audioSettings.MasterVolume);
        AudioManager.SetBackgroundMusicVolume(audioSettings.MusicVolume);
        AudioManager.SetSoundEffectsVolume(audioSettings.SoundEffectsVolume);
    }
}