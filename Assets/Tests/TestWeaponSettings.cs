public class TestWeaponSettings : IWeaponSettings
{
    public WeaponId WeaponId { get; set; }
    public int Damage { get; set; }
    public int StartingAmmo { get; set; }
    public int MaxAmmo { get; set; }
    public int RoundsPerClip { get; set; }
    public int ProjectileCount { get; set; }
    public float ReloadTime { get; set; }
    public float IntervalBetweenShots { get; set; }
    public ProjectileBehaviour ProjectilePrefab { get; set; }
    public float SwapInTime { get; set; }
    public float SwapOutTime { get; set; }
}