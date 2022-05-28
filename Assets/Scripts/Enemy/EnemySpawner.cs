using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class EnemySpawner : ITickable
{
    public event Action<Enemy> OnEnemyDeath;
    public event Action<Enemy, BodyPart> OnEnemyHit;
    public event Action OnEnemyCountChanged;

    public int LiveEnemiesCount => liveEnemies.Count;
    public bool IsSpawning => !waveSpawnEnded;

    float TimeSinceWaveStart => Time.time - currentWaveStartTime;
    IReadOnlyList<SpawnPoint> SpawnPoints => spawnPointCollection.SpawnPoints;

    readonly SpawnPointCollection spawnPointCollection;
    readonly EnemySpawnerSettings settings;
    readonly Queue<float> spawnTimes = new Queue<float>();
    readonly HashSet<EnemyBehaviour> liveEnemies = new HashSet<EnemyBehaviour>();

    EnemySpawnerWaveSettings currentWaveSettings;
    float currentWaveStartTime;
    int spawnedEnemies;
    bool waveSpawnEnded;

    public EnemySpawner (
        SpawnPointCollection spawnPointCollection,
        EnemySpawnerSettings settings
    )
    {
        this.spawnPointCollection = spawnPointCollection;
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

    public void Tick ()
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
            if (liveEnemies.Count >= settings.MaxEnemies)
                return;
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
        var position = new Vector3(spawnPoint.Position.x, 0, spawnPoint.Position.z);
        var enemyBehaviour = GameObject.Instantiate<EnemyBehaviour>(
            GetNextEnemyPrefab(),
            position,
            Quaternion.identity
        );
        enemyBehaviour.OnDeath += HandleEnemyDeath;
        enemyBehaviour.OnHit += HandleEnemyHit;
        spawnedEnemies++;
        liveEnemies.Add(enemyBehaviour);
        OnEnemyCountChanged?.Invoke();
    }

    void HandleEnemyHit (EnemyBehaviour enemy, BodyPart bodyPart)
    {
        OnEnemyHit?.Invoke(enemy.Enemy, bodyPart);
    }

    void HandleEnemyDeath (EnemyBehaviour enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;
        liveEnemies.Remove(enemy);
        OnEnemyCountChanged?.Invoke();
        OnEnemyDeath?.Invoke(enemy.Enemy);
    }

    void EndWaveSpawn ()
    {
        waveSpawnEnded = true;
    }

    EnemyBehaviour GetNextEnemyPrefab () =>
        RandomUtils.WeightedRandom(currentWaveSettings.EnemyChances, x => x.Chance).EnemyPrefab;

    SpawnPoint GetRandomSpawnPoint () => SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)];
}