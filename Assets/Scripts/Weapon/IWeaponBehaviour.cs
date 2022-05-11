using UnityEngine;

public interface IWeaponBehaviour
{
    void SetActive (bool active);
    Transform[] ProjectileSpawnPoints { get; }
    Transform WeaponTransform { get; }
    Animation Animation { get; }
    Weapon Weapon { get; }
    WeaponAnimation WeaponAnimation { get; }
}
