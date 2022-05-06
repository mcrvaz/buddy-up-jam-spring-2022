using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [field: SerializeField] public CurrencySettings CurrencySettings { get; private set; }

    public Currency Currency { get; private set; }

    EnemyWaveManagerBehaviour enemyWaveManager;

    void Awake ()
    {
        enemyWaveManager = FindObjectOfType<EnemyWaveManagerBehaviour>();
        Currency = new Currency(CurrencySettings);
    }

    void Start ()
    {
        enemyWaveManager.WaveManager.OnEnemyDeath += HandleEnemyDeath;
        Currency.Start();
    }

    void HandleEnemyDeath (Enemy enemy)
    {
        Currency.Earn(enemy.CurrencyReward);
    }
}