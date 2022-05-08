using UnityEngine;
using UnityEngine.Audio;

public class GameAudioBehaviour : MonoBehaviour
{
    [field: SerializeField] public AudioSource MusicSource { get; private set; }
    [field: SerializeField] public AudioSource DefaultSoundEffectsSource { get; private set; }
    [field: SerializeField] public AudioClipDatabaseSettings AudioClipDatabase { get; private set; }
    [field: SerializeField] public AudioMixer AudioMixer { get; private set; }

    AudioManager audioManager;

    void Awake ()
    {
        audioManager = new AudioManager(
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
        audioManager.SetMasterVolume(audioSettings.MasterVolume);
        audioManager.SetBackgroundMusicVolume(audioSettings.MusicVolume);
        audioManager.SetSoundEffectsVolume(audioSettings.SoundEffectsVolume);
    }
}