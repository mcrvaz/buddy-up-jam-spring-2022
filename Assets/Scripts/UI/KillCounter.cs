using TMPro;

public class KillCounter
{
    readonly EnemyWaveManager waveManager;
    readonly TextMeshProUGUI ammoText;

    int counter;

    public KillCounter (EnemyWaveManager waveManager, TextMeshProUGUI ammoText)
    {
        this.waveManager = waveManager;
        this.ammoText = ammoText;
        waveManager.OnEnemyDeath += HandleEnemyDeath;
    }

    public void Start ()
    {
        UpdateKillCounter();
    }

    void HandleEnemyDeath (Enemy enemy)
    {
        counter++;
        UpdateKillCounter();
    }

    void UpdateKillCounter ()
    {
        ammoText.text = counter.ToString();
    }
}