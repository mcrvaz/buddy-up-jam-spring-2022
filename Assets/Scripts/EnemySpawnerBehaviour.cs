using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [field: SerializeField] public List<SpawnPoint> SpawnPoints { get; private set; }
    [field: SerializeField] public EnemySpawnerSettings SpawnerSettings { get; private set; }

    public EnemySpawner Spawner { get; private set; }
    public EnemyWaveManager WaveManager { get; private set; }

    void Awake ()
    {
        Spawner = new EnemySpawner(SpawnPoints, SpawnerSettings);
        WaveManager = new EnemyWaveManager(SpawnerSettings, Spawner);
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