public class WeaponSoundManager
{
    readonly Weapon weapon;
    readonly IWeaponSounds weaponSounds;

    public WeaponSoundManager (
        Weapon weapon,
        IWeaponSounds weaponSounds
    )
    {
        this.weapon = weapon;
        this.weaponSounds = weaponSounds;
        weapon.OnShoot += HandleWeaponShoot;
        weapon.OnReloadStart += HandleWeaponReloadStart;
        weapon.OnEmptyAmmoFire += HandleEmptyAmmoFire;
        weapon.OnSwapInStart += HandleSwapInStart;
        weapon.OnSwapOutStart += HandleSwapOutStart;
    }

    void HandleSwapOutStart (float _) => weaponSounds.SwapOut();

    void HandleSwapInStart (float _) => weaponSounds.SwapIn();

    void HandleEmptyAmmoFire () => weaponSounds.EmptyClip();

    void HandleWeaponReloadStart (float _) => weaponSounds.Reload();

    void HandleWeaponShoot () => weaponSounds.Shoot();
}