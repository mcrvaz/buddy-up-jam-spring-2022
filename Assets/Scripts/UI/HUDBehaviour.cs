using UnityEngine;

public class HUDBehaviour : MonoBehaviour
{
    [SerializeField]
    AmmoCounterBehaviour ammoCounter;
    [SerializeField]
    CurrencyCounterBehaviour currencyCounter;
    [SerializeField]
    FadeOutBehaviour fadeOut;
    [SerializeField]
    GameOverBehaviour gameOver;
    [SerializeField]
    HealthCounterBehaviour healthCounter;
    [SerializeField]
    KillCounterBehaviour killCounter;
    [SerializeField]
    PauseUIBehaviour pause;
    [SerializeField]
    WaveIntervalUIBehaviour waveInterval;
}