using System;
using System.Collections;
using UnityEngine;

public class EnemyWaveManager
{
    public event Action OnWaveStarted;
    public event Action OnWaveEnded;
    public event Action OnTimeUntilNextWaveChanged;

    public event Action<Enemy> OnEnemyDeath
    {
        add => spawner.OnEnemyDeath += value;
        remove => spawner.OnEnemyDeath -= value;
    }

    public float TimeUntilNextWave => nextWaveStartTime - Time.time;
    public bool HasMoreWaves => waveIndex < spawnerSettings.Waves.Count;

    readonly EnemySpawnerSettings spawnerSettings;
    readonly EnemySpawner spawner;
    readonly MonoBehaviour coroutineRunner;

    float nextWaveStartTime;
    int waveIndex;

    public EnemyWaveManager (EnemySpawnerSettings spawnerSettings, EnemySpawner spawner, MonoBehaviour coroutineRunner)
    {
        this.spawnerSettings = spawnerSettings;
        this.spawner = spawner;

        spawner.OnEnemyCountChanged += HandleLiveEnemyCountChanged;
        this.coroutineRunner = coroutineRunner;
    }

    public void Start ()
    {
        StartNextWave();
    }

    void StartNextWave ()
    {
        UnityEngine.Debug.Log("starting wave " + (waveIndex + 1));
        spawner.StartWave(waveIndex);
        OnWaveStarted?.Invoke();
    }

    void HandleWaveEnded ()
    {
        OnWaveEnded?.Invoke();

        waveIndex++;
        if (!HasMoreWaves)
        {
            UnityEngine.Debug.Log("No more waves!");
            return;
        }

        nextWaveStartTime = Time.time + spawnerSettings.WaveInterval;
        coroutineRunner.StartCoroutine(WaitForWaveInterval());
    }

    void HandleLiveEnemyCountChanged ()
    {
        if (spawner.LiveEnemiesCount == 0)
            HandleWaveEnded();
    }

    IEnumerator WaitForWaveInterval ()
    {
        while (Time.time < nextWaveStartTime)
        {
            OnTimeUntilNextWaveChanged?.Invoke();
            yield return null;
        }
        StartNextWave();
    }
}