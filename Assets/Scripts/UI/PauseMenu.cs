using UnityEngine;
using UnityEngine.UI;

public class PauseMenu
{
    public bool Enabled
    {
        get => enabled;
        set
        {
            enabled = value;
            if (enabled)
                Enable();
            else
                Disable();
        }
    }

    readonly Slider mouseSensitivitySlider;
    readonly Slider masterVolumeSlider;
    readonly Slider musicVolumeSlider;
    readonly Slider sFXVolumeSlider;
    readonly GameSettings settings;

    bool enabled;

    public PauseMenu (
        Slider mouseSensitivitySlider,
        Slider masterVolumeSlider,
        Slider musicVolumeSlider,
        Slider sFXVolumeSlider,
        GameSettings settings
    )
    {
        this.mouseSensitivitySlider = mouseSensitivitySlider;
        this.masterVolumeSlider = masterVolumeSlider;
        this.musicVolumeSlider = musicVolumeSlider;
        this.sFXVolumeSlider = sFXVolumeSlider;

        mouseSensitivitySlider.onValueChanged.AddListener(HandleMouseSensitivityChanged);
        masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        sFXVolumeSlider.onValueChanged.AddListener(HandleSFXVolumeChanged);
        this.settings = settings;
    }

    public void Toggle ()
    {
        Enabled = !Enabled;
    }

    void Enable ()
    {
        Time.timeScale = 0;
        CursorManager.SetVisible(true);
    }

    void Disable ()
    {
        CursorManager.SetVisible(false);
        Time.timeScale = 1;
    }

    void HandleSFXVolumeChanged (float volume)
    {
        settings.Audio.SoundEffectsVolume = volume;
        settings.RaiseOnUpdated();
    }

    void HandleMusicVolumeChanged (float volume)
    {
        settings.Audio.MusicVolume = volume;
        settings.RaiseOnUpdated();
    }

    void HandleMasterVolumeChanged (float volume)
    {
        settings.Audio.MasterVolume = volume;
        settings.RaiseOnUpdated();
    }

    void HandleMouseSensitivityChanged (float sensitivity)
    {
        settings.MouseSensitivity = sensitivity;
        settings.RaiseOnUpdated();
    }
}