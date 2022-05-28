using System.Collections.Generic;
using UnityEngine;

public class WeaponCollection : MonoBehaviour
{
    [SerializeField] IWeaponBehaviour[] weapons;

    public IReadOnlyList<IWeaponBehaviour> Weapons => weapons;

    void OnValidate ()
    {
        weapons = GetComponentsInChildren<IWeaponBehaviour>();
    }
}