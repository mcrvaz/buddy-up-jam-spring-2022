using UnityEngine;

public interface IWeaponBehaviour
{
    Transform[] ProjectileSpawnPoints { get; }
    Transform WeaponTransform { get; }
    Animation Animation { get; }
    Weapon Weapon { get; }
    WeaponAnimation WeaponAnimation { get; }
}
