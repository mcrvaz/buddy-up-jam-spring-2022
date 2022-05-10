using UnityEngine;

public class AudioClipDatabaseSettings : ScriptableObject
{
    [field: SerializeField] public AudioClip ShotgunFire { get; private set; }
    [field: SerializeField] public AudioClip PlayerFootsteps { get; private set; }
    [field: SerializeField] public AudioClip Ambient { get; private set; }
    [field: SerializeField] public AudioClip ShopPurchase { get; private set; }
    [field: SerializeField] public AudioClip ShopOpen { get; private set; }
    [field: SerializeField] public AudioClip EnemyDeath { get; private set; }
    [field: SerializeField] public AudioClip EnemyHit { get; private set; }
}