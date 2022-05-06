using System;
using System.Collections;
using UnityEngine;

public class EnemyWaveManager
{
    public event Action OnWaveStarted;
    public event Action OnWaveEnded;

    public float TimeUntilNextWave => nextWaveStartTime - Time.time;

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
        spawner.StartWave(waveIndex);
        OnWaveStarted?.Invoke();
    }

    void HandleWaveEnded ()
    {
        OnWaveEnded?.Invoke();

        waveIndex++;
        if (waveIndex >= spawnerSettings.Waves.Count)
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
            yield return null;
        StartNextWave();
    }
}