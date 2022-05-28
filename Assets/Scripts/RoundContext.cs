using System;
using VContainer;
using VContainer.Unity;

public class RoundContext : IInitializable, IStartable, ITickable, IDisposable
{
    [Inject] readonly CurrencyManager currencyManager;
    [Inject] readonly CurrencyCounter currencyCounter;

    [Inject] readonly KillCounter killCounter;

    [Inject] readonly HealthCounter healthCounter;

    [Inject] readonly EnemySpawner enemySpawner;
    [Inject] readonly EnemyWaveManager enemyWaveManager;

    [Inject] readonly AmmoCounter ammoCounter;

    [Inject] readonly PlayerWeapon playerWeapon;

    public void Initialize ()
    {
        currencyCounter.Initialize();
        killCounter.Initialize();
        healthCounter.Initialize();
        ammoCounter.Initialize();
    }

    public void Start ()
    {
        playerWeapon.Start();
        currencyManager.Start();
        enemyWaveManager.Start();
    }

    public void Tick ()
    {
        enemySpawner.Tick();
    }

    public void Dispose () { }
}