using TMPro;
using UnityEngine;
using VContainer;

public class CurrencyCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyCounterText;

    [Inject]
    readonly CurrencyCounter currencyCounter;

    void Start ()
    {
        currencyCounter.OnValueChanged += UpdateText;
        UpdateText(currencyCounter.Value);
    }

    void UpdateText (string text) => currencyCounterText.text = text;
}