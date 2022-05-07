using TMPro;
using UnityEngine;

public class WaveIntervalUIBehaviour : MonoBehaviour
{
    [field: SerializeField] public GameObject WaveIntervalPanel { get; private set; }
    [field: SerializeField] public TextMeshProUGUI WaveCompletedText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TimeLeftText { get; private set; }

    public WaveIntervalUI WaveIntervalUI { get; private set; }

    EnemyWaveManagerBehaviour waveManagerBehaviour;

    void Awake ()
    {
        waveManagerBehaviour = FindObjectOfType<EnemyWaveManagerBehaviour>();
        WaveIntervalPanel.SetActive(false);
    }

    void Start ()
    {
        WaveIntervalUI = new WaveIntervalUI(
            WaveIntervalPanel,
            WaveCompletedText,
            TimeLeftText,
            waveManagerBehaviour.WaveManager
        );
        WaveIntervalUI.Start();
    }
}