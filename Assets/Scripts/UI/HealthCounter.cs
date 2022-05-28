using System;
using VContainer.Unity;

public class HealthCounter : IInitializable, IDisposable
{
    public event Action<string> OnValueChanged;

    public string Value => $"{playerHealth.Current}";

    readonly Health playerHealth;

    public HealthCounter (Health playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    public void Initialize ()
    {
        playerHealth.OnHealthChanged += HandleHealthChanged;
    }

    void HandleHealthChanged (float previous, float current)
    {
        OnValueChanged?.Invoke(Value);
    }

    public void Dispose ()
    {
        playerHealth.OnHealthChanged -= HandleHealthChanged;
    }
}