using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] EnemySpawnerSettings enemySpawnerSettings;
    [SerializeField] CurrencySettings currencySettings;

    protected override void Configure (IContainerBuilder builder)
    {
        builder.RegisterInstance<CurrencySettings>(currencySettings);
        builder.RegisterInstance<EnemySpawnerSettings>(enemySpawnerSettings);

        builder.RegisterComponentInHierarchy<GameConfigurationManager>();
        builder.RegisterComponentInHierarchy<GameAudioBehaviour>();
        builder.RegisterComponentInHierarchy<CoroutineRunner>().AsSelf().As<ICoroutineRunner>();
        builder.RegisterComponentInHierarchy<PlayerBehaviour>();
        builder.RegisterComponentInHierarchy<GameOverBehaviour>();
        builder.RegisterComponentInHierarchy<HealthCounterBehaviour>();
        builder.RegisterComponentInHierarchy<AmmoCounterBehaviour>();
        builder.RegisterComponentInHierarchy<WaveIntervalUIBehaviour>();
        builder.RegisterComponentInHierarchy<BuyItemTextBehaviour>();
        builder.RegisterComponentInHierarchy<FadeOutBehaviour>();
        builder.RegisterComponentInHierarchy<PauseUIBehaviour>();
        builder.RegisterComponentInHierarchy<ShopBehaviour>();
        builder.RegisterComponentInHierarchy<RevolverWeaponBehaviour>();
        builder.RegisterComponentInHierarchy<ShotgunWeaponBehaviour>();
        builder.RegisterComponentInHierarchy<SpawnPointCollection>();

        builder.Register<CurrencyManager>(Lifetime.Scoped);
        builder.Register<CurrencyCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<CurrencyCounterBehaviour>();

        builder.Register<EnemySpawner>(Lifetime.Scoped);
        builder.Register<EnemyWaveManager>(Lifetime.Scoped);

        builder.Register<KillCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<KillCounterBehaviour>();

        builder.RegisterEntryPoint<RoundContext>(Lifetime.Scoped);
    }
}
