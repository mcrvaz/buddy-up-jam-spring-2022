using UnityEngine;

public class WeaponSettings : ScriptableObject
{
    [field: SerializeField] public WeaponId WeaponId { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int StartingAmmo { get; private set; }
    [field: SerializeField] public int MaxAmmo { get; private set; }
    [field: SerializeField] public int RoundsPerClip { get; private set; }
    [field: SerializeField] public int ProjectileCount { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public float IntervalBetweenShots { get; private set; }
    [field: SerializeField] public ProjectileBehaviour ProjectilePrefab { get; private set; }
}