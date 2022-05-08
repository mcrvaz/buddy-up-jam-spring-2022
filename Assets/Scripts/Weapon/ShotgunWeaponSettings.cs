using UnityEngine;

public class ShotgunWeaponSettings : WeaponSettings
{
    [field: SerializeField] public int PelletCount { get; private set; }
    [field: SerializeField] public Vector2 SpreadVerticalAngleRange { get; private set; }
    [field: SerializeField] public Vector2 SpreadHorizontalAngleRange { get; private set; }
}