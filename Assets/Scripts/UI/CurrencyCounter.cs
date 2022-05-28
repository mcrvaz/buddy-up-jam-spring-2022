using System;
using VContainer.Unity;

public class CurrencyCounter : IInitializable, IDisposable
{
    public event Action<string> OnValueChanged;

    public string Value => $"${currencyManager.Current}";

    readonly CurrencyManager currencyManager;

    public CurrencyCounter (CurrencyManager currencyManager)
    {
        this.currencyManager = currencyManager;
    }

    public void Initialize ()
    {
        currencyManager.OnCurrencyChanged += HandleCurrencyChanged;
    }

    void HandleCurrencyChanged (int previous, int current)
    {
        OnValueChanged?.Invoke(Value);
    }

    public void Dispose ()
    {
        currencyManager.OnCurrencyChanged -= HandleCurrencyChanged;
    }
}