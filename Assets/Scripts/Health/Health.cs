using UnityEngine;

public delegate void HealthChangedHandler (float previous, float current);

public class Health
{
    public event HealthChangedHandler OnHealthChanged;

    public float Current { get; private set; }
    public bool IsDead => Current <= 0;
    public float MaxHealth => settings.MaxHealth;

    readonly HealthSettings settings;

    float invulEndTime = float.MinValue;

    public Health (HealthSettings settings)
    {
        this.settings = settings;
    }

    public void Start ()
    {
        Current = settings.InitialHealth;
        OnHealthChanged?.Invoke(0, Current);
    }

    public bool TakeDamage (float damage)
    {
        if (Time.time < invulEndTime)
            return false;
        invulEndTime = Time.time + settings.InvulnerabilityTime;
        var previous = Current;
        Current = Mathf.Max(0, Current - damage);
        OnHealthChanged?.Invoke(previous, Current);
        return true;
    }

    public void RestoreHealth (float health)
    {
        var previous = Current;
        Current = Mathf.Min(settings.MaxHealth, Current + health);
        OnHealthChanged?.Invoke(previous, Current);
    }
}