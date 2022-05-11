using TMPro;
using UnityEngine;

public class AmmoCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    public AmmoCounter AmmoCounter { get; private set; }

    private PlayerBehaviour playerBehaviour;

    void Awake ()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    void Start ()
    {
        AmmoCounter = new AmmoCounter(playerBehaviour.PlayerWeapon, ammoText);
        AmmoCounter.Start();
    }
}