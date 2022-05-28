using TMPro;
using UnityEngine;
using VContainer;

public class KillCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCounterText;

    [Inject]
    readonly KillCounter killCounter;

    void Start ()
    {
        killCounter.OnValueChanged += UpdateText;
        UpdateText(killCounter.Value);
    }

    void UpdateText (string text) => killCounterText.text = text;
}