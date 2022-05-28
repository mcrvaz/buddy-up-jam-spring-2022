using System;
using VContainer;
using VContainer.Unity;

public class RoundContext : IInitializable, IStartable, ITickable, IDisposable
{
    [Inject] readonly CurrencyManager currencyManager;
    [Inject] readonly CurrencyCounter currencyCounter;

    [Inject] readonly KillCounter killCounter;

    [Inject] readonly EnemySpawner enemySpawner;
    [Inject] readonly EnemyWaveManager enemyWaveManager;

    public void Initialize ()
    {
        currencyCounter.Initialize();
        killCounter.Initialize();
    }

    public void Start ()
    {
        currencyManager.Start();
        enemyWaveManager.Start();
    }

    public void Tick ()
    {
        enemySpawner.Tick();
    }

    public void Dispose () { }
}