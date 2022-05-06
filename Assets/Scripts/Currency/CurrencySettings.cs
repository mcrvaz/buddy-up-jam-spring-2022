using UnityEngine;

public class CurrencySettings : ScriptableObject
{
    [field:SerializeField] public int InitialCurrency { get; set; }
}