using TMPro;
using UnityEngine;
using VContainer;

public class HealthCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    [Inject]
    readonly HealthCounter healthCounter;

    void Start ()
    {
        healthCounter.OnValueChanged += UpdateText;
        UpdateText(healthCounter.Value);
    }

    void UpdateText (string text) => healthText.text = text;
}