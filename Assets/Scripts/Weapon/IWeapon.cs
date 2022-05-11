using System;

public interface IWeapon
{
    event Action<float> OnWeaponSwapInStart;
    event Action<float> OnWeaponSwapOutStart;
    event Action<IWeapon> OnSwapOutEnded;
    event Action<IWeapon> OnSwapInEnded;

    event Action OnAmmoUpdated;
    event Action<float> OnReloadStart;
    event Action OnReloadEnd;
    event Action OnShoot;
    event Action OnEmptyAmmoFire;

    WeaponId WeaponId { get; }
    int CurrentAmmo { get; }
    int MaxAmmo { get; }
    int RoundsInClip { get; }
    int RoundsPerClip { get; }
    int Damage { get; }
    float SwapInTime { get; }
    float SwapOutTime { get; }

    void Reload ();
    void RestoreAmmo (int amount);
    void Shoot ();
    void Update ();
    void SetAsActive ();
    void SwapIn ();
    void SwapOut ();
}
