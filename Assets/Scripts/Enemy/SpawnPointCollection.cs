using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCollection : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;

    public IReadOnlyList<SpawnPoint> SpawnPoints => spawnPoints;

    void OnValidate ()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
}