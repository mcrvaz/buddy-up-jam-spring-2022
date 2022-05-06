using TMPro;
using UnityEngine;

public class HealthCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    public HealthCounter HealthCounter { get; private set; }

    private PlayerBehaviour playerBehaviour;

    void Awake ()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        HealthCounter = new HealthCounter(playerBehaviour.Health, healthText);
    }
}