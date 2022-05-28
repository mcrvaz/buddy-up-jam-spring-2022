using TMPro;
using UnityEngine;
using VContainer;

public class AmmoCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    [Inject]
    readonly AmmoCounter ammoCounter;

    void Start ()
    {
        ammoCounter.OnValueChanged += UpdateText;
        UpdateText(ammoCounter.Value);
    }

    void UpdateText (string text) => ammoText.text = text;
}