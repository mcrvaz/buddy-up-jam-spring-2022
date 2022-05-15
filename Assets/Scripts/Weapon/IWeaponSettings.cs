public interface IWeaponSettings
{
    WeaponId WeaponId { get; }
    int Damage { get; }
    int StartingAmmo { get; }
    int MaxAmmo { get; }
    int RoundsPerClip { get; }
    int ProjectileCount { get; }
    float ReloadTime { get; }
    float IntervalBetweenShots { get; }
    ProjectileBehaviour ProjectilePrefab { get; }
    float SwapInTime { get; }
    float SwapOutTime { get; }
}
