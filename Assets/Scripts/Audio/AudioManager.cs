using UnityEngine;
using UnityEngine.Audio;

public class AudioManager
{
    const string PLAYER_FOOTSTEPS_PITCH = "PlayerFootstepsPitch";

    const string MASTER_VOLUME = "MasterVolume";
    const string MUSIC_VOLUME = "BackgroundMusicVolume";
    const string SFX_VOLUME = "SoundEffectsVolume";

    const float MIN_PITCH_VARIATION = 0.9f;
    const float MAX_PITCH_VARIATION = 1.1f;

    readonly AudioSource musicSource;
    readonly AudioSource ambientSource;
    readonly AudioSource defaultSoundEffectsSource;
    readonly AudioClipDatabaseSettings audioClipDatabase;
    readonly AudioMixer masterMixer;
    readonly AudioMixer musicMixer;
    readonly AudioMixer soundEffectsMixer;

    public AudioManager (
        AudioSource musicSource,
        AudioSource ambientSource,
        AudioSource defaultSoundEffectsSource,
        AudioClipDatabaseSettings audioClipDatabase,
        AudioMixer mixerAsset
    )
    {
        this.musicSource = musicSource;
        this.ambientSource = ambientSource;
        this.defaultSoundEffectsSource = defaultSoundEffectsSource;
        this.audioClipDatabase = audioClipDatabase;

        masterMixer = mixerAsset.FindMatchingGroups("Master")[0].audioMixer;
        musicMixer = mixerAsset.FindMatchingGroups("Master/BackgroundMusic")[0].audioMixer;
        soundEffectsMixer = mixerAsset.FindMatchingGroups("Master/SoundEffects")[0].audioMixer;
    }

    public void Start ()
    {
        PlayMusic();
        PlayAmbient();
    }

    public void PlayShopPurchase (AudioSource source) =>
        Play(source, audioClipDatabase.ShopPurchase);

    public void PlayEnemyDeath (AudioSource source) =>
        Play(source, audioClipDatabase.EnemyDeath);

    public void PlayEnemyHit (AudioSource source) =>
        Play(source, audioClipDatabase.EnemyHit);

    public void PlayFootsteps (AudioSource source) =>
        PlayWithPitchVariation(source, audioClipDatabase.PlayerFootsteps, PLAYER_FOOTSTEPS_PITCH);

    public void PlayEmptyAmmo (AudioSource source) =>
        Play(source, audioClipDatabase.EmptyAmmo);

    public void PlayPlayerHit (AudioSource audioSource) =>
        Play(audioSource, audioClipDatabase.PlayerHit);

    public void PlayPlayerDeath (AudioSource audioSource) =>
        Play(audioSource, audioClipDatabase.PlayerDeath);

    public void PlayShotgunReload (AudioSource source) =>
        Play(source, audioClipDatabase.ShotgunReload);

    public void PlayShotgunFire (AudioSource source) =>
        Play(source, audioClipDatabase.ShotgunFire);

    public void PlayShotgunSwapIn (AudioSource source) =>
        Play(source, audioClipDatabase.ShotgunSwapIn);

    public void PlayShotgunSwapOut (AudioSource source) =>
        Play(source, audioClipDatabase.ShotgunSwapOut);

    public void PlayRevolverReload (AudioSource source) =>
        Play(source, audioClipDatabase.RevolverReload);

    public void PlayRevolverFire (AudioSource source) =>
        Play(source, audioClipDatabase.RevolverFire);

    public void PlayRevolverSwapIn (AudioSource source) =>
        Play(source, audioClipDatabase.RevolverSwapIn);

    public void PlayRevolverSwapOut (AudioSource source) =>
        Play(source, audioClipDatabase.RevolverSwapOut);

    public void PlayMusic () =>
        PlayLooping(musicSource, audioClipDatabase.BackgroundMusic);

    public void PlayAmbient () =>
        PlayLooping(ambientSource, audioClipDatabase.Ambient);

    public void SetMasterVolume (float volume)
    {
        masterMixer.SetFloat(MASTER_VOLUME, GetVolumeRatio(volume));
    }

    public void SetSoundEffectsVolume (float volume)
    {
        soundEffectsMixer.SetFloat(SFX_VOLUME, GetVolumeRatio(volume));
    }

    public void SetBackgroundMusicVolume (float volume)
    {
        musicMixer.SetFloat(MUSIC_VOLUME, GetVolumeRatio(volume));
    }

    float GetVolumeRatio (float volume)
    {
        var db = (1 - Mathf.Sqrt(volume)) * -80f;
        return db;
    }

    bool PlayWithPitchVariation (
        AudioSource source,
        AudioClip clip,
        string pitchParam,
        float? pitch = null
    )
    {
        if (source == null)
            source = defaultSoundEffectsSource;
        source.outputAudioMixerGroup.audioMixer.SetFloat(
            pitchParam,
            pitch ?? Random.Range(MIN_PITCH_VARIATION, MAX_PITCH_VARIATION)
        );
        return Play(source, clip);
    }

    bool Play (AudioSource source, AudioClip clip, float volume = 1)
    {
        if (source == null)
            source = defaultSoundEffectsSource;

        source.loop = false;
        source.PlayOneShot(clip, volume);
        return true;
    }

    bool PlayLooping (AudioSource source, AudioClip clip, float volume = 1)
    {
        if (source == null)
            source = defaultSoundEffectsSource;

        source.loop = true;
        source.clip = clip;
        source.volume = volume;
        source.Play();
        return true;
    }
}