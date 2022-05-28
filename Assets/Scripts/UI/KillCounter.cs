using System;
using VContainer.Unity;

public class KillCounter : IInitializable, IDisposable
{
    public event Action<string> OnValueChanged;

    public string Value => $"{counter}/1000";

    readonly EnemyWaveManager waveManager;

    int counter;

    public KillCounter (EnemyWaveManager waveManager)
    {
        this.waveManager = waveManager;
    }

    public void Initialize ()
    {
        waveManager.OnEnemyDeath += HandleEnemyDeath;
    }

    void HandleEnemyDeath (Enemy enemy)
    {
        counter++;
        UpdateKillCounter();
    }

    void UpdateKillCounter ()
    {
        OnValueChanged?.Invoke(Value);
    }

    public void Dispose ()
    {
        waveManager.OnEnemyDeath -= HandleEnemyDeath;
    }
}