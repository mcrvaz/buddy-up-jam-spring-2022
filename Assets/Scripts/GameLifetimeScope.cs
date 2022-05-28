using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] EnemySpawnerSettings enemySpawnerSettings;
    [SerializeField] CurrencySettings currencySettings;
    [SerializeField] PlayerSettings playerSettings;

    protected override void Configure (IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<RoundContext>(Lifetime.Scoped);

        builder.RegisterInstance<PlayerSettings>(playerSettings);
        builder.RegisterInstance<HealthSettings>(playerSettings.HealthSettings);
        builder.RegisterInstance<CurrencySettings>(currencySettings);
        builder.RegisterInstance<EnemySpawnerSettings>(enemySpawnerSettings);

        builder.RegisterComponentInHierarchy<CoroutineRunner>().AsSelf().As<ICoroutineRunner>();

        builder.RegisterComponentInHierarchy<GameConfigurationManager>();
        builder.RegisterComponentInHierarchy<GameAudioBehaviour>();
        builder.RegisterComponentInHierarchy<PlayerBehaviour>();
        builder.RegisterComponentInHierarchy<GameOverBehaviour>();
        builder.RegisterComponentInHierarchy<AmmoCounterBehaviour>();
        builder.RegisterComponentInHierarchy<WaveIntervalUIBehaviour>();
        builder.RegisterComponentInHierarchy<BuyItemTextBehaviour>();
        builder.RegisterComponentInHierarchy<FadeOutBehaviour>();
        builder.RegisterComponentInHierarchy<PauseUIBehaviour>();
        builder.RegisterComponentInHierarchy<ShopBehaviour>();
        builder.RegisterComponentInHierarchy<RevolverWeaponBehaviour>();
        builder.RegisterComponentInHierarchy<ShotgunWeaponBehaviour>();

        builder.RegisterComponentInHierarchy<WeaponCollection>();
        builder.RegisterComponentInHierarchy<SpawnPointCollection>();

        builder.Register<InputManager>(Lifetime.Scoped);
        builder.Register<PlayerAction>(Lifetime.Scoped);
        builder.Register<PlayerWeapon>(Lifetime.Scoped);

        builder.Register<CurrencyManager>(Lifetime.Scoped);
        builder.Register<CurrencyCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<CurrencyCounterBehaviour>();

        builder.Register<EnemySpawner>(Lifetime.Scoped);
        builder.Register<EnemyWaveManager>(Lifetime.Scoped);

        builder.Register<KillCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<KillCounterBehaviour>();

        builder.Register<HealthCounter>(Lifetime.Scoped);
        builder.Register<Health>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<HealthCounterBehaviour>();

        builder.Register<AmmoCounter>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<AmmoCounterBehaviour>();
    }
}
