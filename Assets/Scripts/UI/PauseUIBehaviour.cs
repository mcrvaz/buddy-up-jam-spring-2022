using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PauseUIBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject PauseMenu { get; private set; }
    [field: SerializeField] public Slider MouseSensitivitySlider { get; private set; }
    [field: SerializeField] public Slider MasterVolumeSlider { get; private set; }
    [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
    [field: SerializeField] public Slider SFXVolumeSlider { get; private set; }
    [field: SerializeField] public Button QuitToDesktopButton { get; private set; }

    [Inject]
    GameConfigurationManager configurationManager;

    PauseMenu pauseMenu;

    void Awake ()
    {
        pauseMenu = new PauseMenu(
            MouseSensitivitySlider,
            MasterVolumeSlider,
            MusicVolumeSlider,
            SFXVolumeSlider,
            QuitToDesktopButton,
            configurationManager.Settings
        );
    }

    void Update ()
    {
        if (InputManager.Instance.GetCancelDown())
        {
            pauseMenu.Toggle();
            PauseMenu.SetActive(pauseMenu.Enabled);
        }
    }
}