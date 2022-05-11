using TMPro;

public class AmmoCounter
{
    readonly PlayerWeapon playerWeapon;
    readonly TextMeshProUGUI ammoText;

    IWeapon activeWeapon;

    public AmmoCounter (PlayerWeapon playerWeapon, TextMeshProUGUI ammoText)
    {
        this.playerWeapon = playerWeapon;
        this.ammoText = ammoText;
        playerWeapon.OnWeaponChanged += HandleWeaponChanged;
    }

    public void Start ()
    {
        HandleWeaponChanged();
    }

    void HandleWeaponChanged ()
    {
        if (activeWeapon != null)
            activeWeapon.OnAmmoUpdated -= UpdateAmmoCounter;

        activeWeapon = playerWeapon.ActiveWeapon;
        activeWeapon.OnAmmoUpdated += UpdateAmmoCounter;
        UpdateAmmoCounter();
    }

    void UpdateAmmoCounter ()
    {
        ammoText.text = $"{activeWeapon.RoundsInClip}/{activeWeapon.CurrentAmmo}";
    }
}