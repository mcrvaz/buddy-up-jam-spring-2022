using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    public event Action<Enemy> OnEnemyDeath;
    public event Action OnEnemyCountChanged;

    public int LiveEnemiesCount => liveEnemies.Count;

    float TimeSinceWaveStart => Time.time - currentWaveStartTime;

    readonly IReadOnlyList<SpawnPoint> spawnPoints;
    readonly EnemySpawnerSettings settings;
    readonly Queue<float> spawnTimes = new Queue<float>();
    readonly HashSet<EnemyBehaviour> liveEnemies = new HashSet<EnemyBehaviour>();

    EnemySpawnerWaveSettings currentWaveSettings;
    float currentWaveStartTime;
    int spawnedEnemies;
    bool waveSpawnEnded;

    public EnemySpawner (IReadOnlyList<SpawnPoint> spawnPoints, EnemySpawnerSettings settings)
    {
        this.spawnPoints = spawnPoints;
        this.settings = settings;
    }

    public void StartWave (int wave)
    {
        spawnedEnemies = 0;
        currentWaveSettings = settings.Waves[wave];
        currentWaveStartTime = Time.time;
        liveEnemies.Clear();
        spawnTimes.Clear();
        waveSpawnEnded = false;
        EvaluateSpawnTimes();
    }

    public void Update ()
    {
        if (waveSpawnEnded)
            return;

        if (spawnedEnemies >= currentWaveSettings.EnemyCount)
        {
            EndWaveSpawn();
            return;
        }

        float time = TimeSinceWaveStart;
        while (spawnTimes.Count > 0 && time > spawnTimes.Peek())
        {
            spawnTimes.Dequeue();
            SpawnEnemy();
        }
    }

    void EvaluateSpawnTimes ()
    {
        for (int i = 0; i < currentWaveSettings.EnemyCount; i++)
        {
            var ratio = i / (float)currentWaveSettings.EnemyCount;
            float time = currentWaveSettings.Duration * currentWaveSettings.SpawnRate.Evaluate(ratio);
            spawnTimes.Enqueue(time);
        }
    }

    void SpawnEnemy ()
    {
        var spawnPoint = GetRandomSpawnPoint();
        var enemy = GameObject.Instantiate<EnemyBehaviour>(
            GetNextEnemyPrefab(),
            spawnPoint.Position,
            Quaternion.identity
        );
        enemy.OnDeath += HandleEnemyDeath;
        spawnedEnemies++;
        liveEnemies.Add(enemy);
        OnEnemyCountChanged?.Invoke();
    }

    void HandleEnemyDeath (EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.OnDeath -= HandleEnemyDeath;
        liveEnemies.Remove(enemyBehaviour);
        OnEnemyCountChanged?.Invoke();
        OnEnemyDeath?.Invoke(enemyBehaviour.Enemy);
    }

    void EndWaveSpawn ()
    {
        waveSpawnEnded = true;
    }

    EnemyBehaviour GetNextEnemyPrefab () =>
        RandomUtils.WeightedRandom(currentWaveSettings.EnemyChances, x => x.Chance).EnemyPrefab;

    SpawnPoint GetRandomSpawnPoint () => spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
}