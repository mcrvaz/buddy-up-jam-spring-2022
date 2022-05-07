using TMPro;
using UnityEngine;

public class WaveIntervalUI
{
    const float WAVE_ENDED_TEXT_DURATION = 2f;
    const string TIME_LEFT_TEXT = "Next wave starts in {0}";
    const string WAVE_COMPLETED_TEXT = "Wave completed!";
    const string VICTORY_TEXT = "Victory!";

    readonly GameObject waveIntervalPanel;
    readonly TextMeshProUGUI waveCompletedText;
    readonly TextMeshProUGUI timeLeftText;
    readonly EnemyWaveManager waveManager;

    float waveEndTime;

    public WaveIntervalUI (
        GameObject waveIntervalPanel,
        TextMeshProUGUI waveCompletedText,
        TextMeshProUGUI timeLeftText,
        EnemyWaveManager waveManager
    )
    {
        this.waveCompletedText = waveCompletedText;
        this.timeLeftText = timeLeftText;
        this.waveManager = waveManager;
        this.waveIntervalPanel = waveIntervalPanel;
    }

    public void Start ()
    {
        waveManager.OnWaveStarted += HandleWaveStarted;
        waveManager.OnWaveEnded += HandleWaveEnded;
        waveManager.OnTimeUntilNextWaveChanged += HandleTimeChanged;
    }

    void HandleTimeChanged ()
    {
        if (Time.time >= waveEndTime + WAVE_ENDED_TEXT_DURATION)
        {
            waveCompletedText.SetActive(false);
            timeLeftText.SetActive(true);
        }

        float timeLeft = waveManager.TimeUntilNextWave;
        string format = timeLeft <= 3 ? "0.0" : "0";
        string formattedTimeLeft = waveManager.TimeUntilNextWave.ToString(format);
        timeLeftText.text = string.Format(TIME_LEFT_TEXT, formattedTimeLeft);
        timeLeftText.SetActive(true);
    }

    void HandleWaveStarted ()
    {
        waveIntervalPanel.SetActive(false);
    }

    void HandleWaveEnded ()
    {
        waveEndTime = Time.time;
        waveIntervalPanel.SetActive(true);
        waveCompletedText.SetActive(true);
        timeLeftText.SetActive(false);

        if (waveManager.HasMoreWaves)
            waveCompletedText.text = WAVE_COMPLETED_TEXT;
        else
            waveCompletedText.text = VICTORY_TEXT;
    }
}