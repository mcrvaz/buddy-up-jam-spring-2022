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
        Time.fixedDeltaTime = 1f / Settings.MaxFPS;
    }

    void Start ()
    {
        GameAudio.ApplySettings(Settings);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        if (InputManager.Instance.GetCancelDown())
            Cursor.visible = !Cursor.visible;
    }
}