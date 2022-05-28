using TMPro;
using UnityEngine;
using VContainer;

public class HealthCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    public HealthCounter HealthCounter { get; private set; }

    [Inject]
    PlayerBehaviour playerBehaviour;

    void Start ()
    {
        HealthCounter = new HealthCounter(playerBehaviour.Health, healthText);
    }
}