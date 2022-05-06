using TMPro;
using UnityEngine;

public class CurrencyCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyText;

    public CurrencyCounter CurrencyCounter { get; private set; }

    CurrencyManager currencyBehaviour;

    void Awake ()
    {
        currencyBehaviour = FindObjectOfType<CurrencyManager>();
    }

    void Start ()
    {
        CurrencyCounter = new CurrencyCounter(currencyBehaviour.Currency, currencyText);
    }
}