using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    float TimeSinceWaveStart => Time.time - currentWaveStartTime;

    readonly List<SpawnPoint> spawnPoints;
    readonly EnemySpawnerSettings settings;
    readonly Queue<float> spawnTimes = new Queue<float>();

    EnemySpawnerWaveSettings currentWaveSettings;
    float currentWaveStartTime;
    float currentWaveEndTime;
    int spawnedEnemies;

    public EnemySpawner (List<SpawnPoint> spawnPoints, EnemySpawnerSettings settings)
    {
        this.spawnPoints = spawnPoints;
        this.settings = settings;
    }

    public void StartFirstWave () => StartWave(0);
    public void StartWave (int wave)
    {
        currentWaveSettings = settings.Waves[wave];
        currentWaveStartTime = Time.time;
        currentWaveEndTime = currentWaveStartTime + currentWaveSettings.Duration;
        EvaluateSpawnTimes();
    }

    public void Update ()
    {
        if (Time.time > currentWaveEndTime || spawnedEnemies >= currentWaveSettings.EnemyCount)
            return;

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
        spawnedEnemies++;
    }

    EnemyBehaviour GetNextEnemyPrefab () =>
        RandomUtils.WeightedRandom(currentWaveSettings.EnemyChances, x => x.Chance).EnemyPrefab;

    SpawnPoint GetRandomSpawnPoint () => spawnPoints[Random.Range(0, spawnPoints.Count)];
}