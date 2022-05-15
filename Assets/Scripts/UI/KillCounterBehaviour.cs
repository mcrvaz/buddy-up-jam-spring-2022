using TMPro;
using UnityEngine;

public class KillCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCounterText;

    public KillCounter KillCounter { get; private set; }

    EnemyWaveManagerBehaviour playerBehaviour;

    void Awake ()
    {
        playerBehaviour = FindObjectOfType<EnemyWaveManagerBehaviour>();
    }

    void Start ()
    {
        KillCounter = new KillCounter(playerBehaviour.WaveManager, killCounterText);
        KillCounter.Start();
    }
}