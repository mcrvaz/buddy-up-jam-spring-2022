using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [field: SerializeField] public List<SpawnPoint> SpawnPoints { get; private set; }
    [field: SerializeField] public EnemySpawnerSettings SpawnerSettings { get; private set; }

    public EnemySpawner Spawner { get; private set; }

    void Awake ()
    {
        Spawner = new EnemySpawner(SpawnPoints, SpawnerSettings);
    }

    void Start ()
    {
        Spawner.StartFirstWave();
    }

    void Update ()
    {
        Spawner.Update();
    }
}