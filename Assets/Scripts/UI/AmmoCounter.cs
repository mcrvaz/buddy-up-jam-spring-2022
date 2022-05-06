using TMPro;

public class AmmoCounter
{
    readonly Weapon playerWeapon;
    readonly TextMeshProUGUI ammoText;

    public AmmoCounter (Weapon playerWeapon, TextMeshProUGUI ammoText)
    {
        this.playerWeapon = playerWeapon;
        this.ammoText = ammoText;
        playerWeapon.OnAmmoUpdated += UpdateAmmoCounter;
        UpdateAmmoCounter();
    }

    void UpdateAmmoCounter ()
    {
        ammoText.text = $"{playerWeapon.RoundsInClip}/{playerWeapon.CurrentAmmo}";
    }
}