using TMPro;

public class CurrencyCounter
{
    readonly Currency currency;
    readonly TextMeshProUGUI healthText;

    public CurrencyCounter (Currency currency, TextMeshProUGUI healthText)
    {
        this.currency = currency;
        this.healthText = healthText;
        currency.OnCurrencyChanged += HandleCurrencyChanged;
        HandleCurrencyChanged(0, currency.Current);
    }

    void HandleCurrencyChanged (int previous, int current)
    {
        healthText.text = $"${current}";
    }
}