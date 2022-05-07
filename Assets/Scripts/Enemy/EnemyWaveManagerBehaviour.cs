using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManagerBehaviour : MonoBehaviour
{
    [field: SerializeField] public EnemySpawnerSettings SpawnerSettings { get; private set; }

    public EnemySpawner Spawner { get; private set; }
    public EnemyWaveManager WaveManager { get; private set; }

    SpawnPoint[] spawnPoints;

    void Awake ()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        Spawner = new EnemySpawner(spawnPoints, SpawnerSettings);
        WaveManager = new EnemyWaveManager(SpawnerSettings, Spawner, this);
    }

    void Start ()
    {
        WaveManager.Start();
    }

    void Update ()
    {
        Spawner.Update();
    }
}