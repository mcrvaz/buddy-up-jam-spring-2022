public delegate void CurrencyChangedHandler (int previous, int current);

public class Currency
{
    public event CurrencyChangedHandler OnCurrencyChanged;

    public int Current { get; private set; }

    readonly CurrencySettings settings;

    public Currency (CurrencySettings settings)
    {
        this.settings = settings;
    }

    public void Start ()
    {
        Current = settings.InitialCurrency;
        OnCurrencyChanged?.Invoke(0, Current);
    }

    public bool Spend (int value)
    {
        if (Current < value)
            return false;
        var previous = Current;
        Current -= value;
        OnCurrencyChanged?.Invoke(previous, Current);
        return true;
    }

    public void Earn (int value)
    {
        var previous = Current;
        Current += value;
        OnCurrencyChanged?.Invoke(previous, Current);
    }
}
