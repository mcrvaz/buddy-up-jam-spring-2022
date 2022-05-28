using TMPro;
using UnityEngine;
using VContainer;

public class AmmoCounterBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    public AmmoCounter AmmoCounter { get; private set; }

    [Inject]
    readonly PlayerBehaviour playerBehaviour;

    void Start ()
    {
        AmmoCounter = new AmmoCounter(playerBehaviour.PlayerWeapon, ammoText);
        AmmoCounter.Start();
    }
}