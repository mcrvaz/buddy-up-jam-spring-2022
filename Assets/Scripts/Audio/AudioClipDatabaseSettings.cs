using UnityEngine;

public class AudioClipDatabaseSettings : ScriptableObject
{
    [field: Header("Weapons")]
    [field: SerializeField] public AudioClip EmptyAmmo { get; private set; }
    [field: Header("Shotgun")]
    [field: SerializeField] public AudioClip ShotgunFire { get; private set; }
    [field: SerializeField] public AudioClip ShotgunReload { get; private set; }
    [field: SerializeField] public AudioClip ShotgunSwapIn { get; private set; }
    [field: SerializeField] public AudioClip ShotgunSwapOut { get; private set; }
    [field: Header("Revolver")]
    [field: SerializeField] public AudioClip RevolverFire { get; private set; }
    [field: SerializeField] public AudioClip RevolverReload { get; private set; }
    [field: SerializeField] public AudioClip RevolverSwapIn { get; private set; }
    [field: SerializeField] public AudioClip RevolverSwapOut { get; private set; }
    [field: Header("Player")]
    [field: SerializeField] public AudioClip PlayerDeath { get; private set; }
    [field: SerializeField] public AudioClip PlayerHit { get; private set; }
    [field: SerializeField] public AudioClip PlayerFootsteps { get; private set; }
    [field: Header("Enemy")]
    [field: SerializeField] public AudioClip EnemyDeath { get; private set; }
    [field: SerializeField] public AudioClip EnemyHit { get; private set; }
    [field: Header("Shop")]
    [field: SerializeField] public AudioClip ShopPurchase { get; private set; }
    [field: SerializeField] public AudioClip ShopOpen { get; private set; }
    [field: Header("Other")]
    [field: SerializeField] public AudioClip BackgroundMusic { get; private set; }
    [field: SerializeField] public AudioClip Ambient { get; private set; }
}
