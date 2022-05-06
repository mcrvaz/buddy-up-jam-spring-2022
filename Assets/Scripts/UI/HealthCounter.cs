using TMPro;

public class HealthCounter
{
    readonly Health playerHealth;
    readonly TextMeshProUGUI healthText;

    public HealthCounter (Health playerHealth, TextMeshProUGUI healthText)
    {
        this.playerHealth = playerHealth;
        this.healthText = healthText;
        playerHealth.OnHealthChanged += HandleHealthChanged;
        HandleHealthChanged(0, playerHealth.Current);
    }

    void HandleHealthChanged (float previous, float current)
    {
        healthText.text = $"{current}";
    }
}