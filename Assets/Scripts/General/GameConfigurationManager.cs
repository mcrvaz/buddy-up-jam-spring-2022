using UnityEngine;

public class GameConfigurationManager : MonoBehaviour
{
    [field: SerializeField] public GameSettings Settings { get; private set; }
    [field: SerializeField] public GameAudioBehaviour GameAudio { get; private set; }

    void Awake ()
    {
        QualitySettings.vSyncCount = Settings.VSync ? 0 : 1;
        Application.targetFrameRate = Settings.MaxFPS;
        Screen.fullScreenMode = Settings.Fullscreen;
        Time.fixedDeltaTime = 1f / 60f;
    }

    void Start ()
    {
        GameAudio.ApplySettings(Settings);
        CursorManager.SetVisible(false);
    }
}