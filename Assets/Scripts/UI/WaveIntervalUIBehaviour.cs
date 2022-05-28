using TMPro;
using UnityEngine;
using VContainer;

public class WaveIntervalUIBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject WaveIntervalPanel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI WaveCompletedText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TimeLeftText { get; private set; }

    public WaveIntervalUI WaveIntervalUI { get; private set; }

    [Inject]
    EnemyWaveManager waveManager;

    void Awake ()
    {
        WaveIntervalPanel.SetActive(false);
    }

    void Start ()
    {
        WaveIntervalUI = new WaveIntervalUI(
            WaveIntervalPanel,
            WaveCompletedText,
            TimeLeftText,
            waveManager
        );
        WaveIntervalUI.Start();
    }
}