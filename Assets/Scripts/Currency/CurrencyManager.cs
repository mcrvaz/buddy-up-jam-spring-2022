using System;
using VContainer.Unity;

public delegate void CurrencyChangedHandler (int previous, int current);

public class CurrencyManager : IStartable, IDisposable
{
    public event CurrencyChangedHandler OnCurrencyChanged;

    public int Current { get; private set; }

    readonly CurrencySettings settings;
    readonly EnemyWaveManager enemyWaveManager;

    public CurrencyManager (
        CurrencySettings settings,
        EnemyWaveManager enemyWaveManager
    )
    {
        this.settings = settings;
        this.enemyWaveManager = enemyWaveManager;
    }

    public void Start ()
    {
        Current = settings.InitialCurrency;
        OnCurrencyChanged?.Invoke(0, Current);

        enemyWaveManager.OnEnemyDeath += HandleEnemyDeath;
    }

    public bool Spend (int value)
    {
        if (Current < value)
            return false;
        var previous = Current;
        Current -= value;
        OnCurrencyChanged?.Invoke(previous, Current);
        return true;
    }

    public void Earn (int value)
    {
        var previous = Current;
        Current += value;
        OnCurrencyChanged?.Invoke(previous, Current);
    }

    void HandleEnemyDeath (Enemy enemy)
    {
        Earn(enemy.CurrencyReward);
    }

    public void Dispose ()
    {
        enemyWaveManager.OnEnemyDeath -= HandleEnemyDeath;
    }
}