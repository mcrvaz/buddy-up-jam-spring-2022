using UnityEngine;
using UnityEngine.UI;

public class PauseUIBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject PauseMenu { get; private set; }
    [field: SerializeField] public Slider MouseSensitivitySlider { get; private set; }
    [field: SerializeField] public Slider MasterVolumeSlider { get; private set; }
    [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
    [field: SerializeField] public Slider SFXVolumeSlider { get; private set; }

    PauseMenu pauseMenu;

    void Awake ()
    {
        var settings = FindObjectOfType<GameConfigurationManager>().Settings;
        pauseMenu = new PauseMenu(
            MouseSensitivitySlider,
            MasterVolumeSlider,
            MusicVolumeSlider,
            SFXVolumeSlider,
            settings
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