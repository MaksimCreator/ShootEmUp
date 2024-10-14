using ShootEmUp.Model;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [SerializeField] private BulletConfig _characterBulletConfig;
    [SerializeField] private BulletConfig _enemyBulletConfig;
    [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
    [SerializeField] private EnemyMovementConfig _enemyMovementConfig;
    [SerializeField] private CharacterConfig _characterConfig;

    private DataSave _dataService;

    public override void InstallBindings()
    {
        ConfigRegistary();
        WalletRegistary();
        DataRegistary();
    }

    private void WalletRegistary()
    {
        Container
          .Bind<IWallet>()
          .To<Wallet>()
          .FromNew()
          .AsSingle();
    }

    private void DataRegistary()
    {
        DataServiceRegistary();
        GameDataRegistary();
        SingelPlayerDataRegistary();
    }

    private void ConfigRegistary()
    {
        CharacterBulletConfigRegistary();
        EnemyBulletConfigRegistary();
        EnemySpawnerConfigRegistary();
        EnemyMovementConfigRegistary();
        CharacterConfigRegistary();
    }

    private void SingelPlayerDataRegistary()
    {
        Container.
            Bind<SingelPlayerData>()
            .FromNew()
            .AsSingle();
    }

    private void GameDataRegistary()
    {
        Container
           .Bind<GameData>()
           .FromInstance(_dataService.Load())
           .AsSingle();
    }

    private void DataServiceRegistary()
    {
        _dataService = new DataSave();

        Container
           .Bind<IDataService>()
           .To<DataSave>()
           .FromInstance(_dataService)
           .AsSingle();
    }

    private void CharacterBulletConfigRegistary()
    {
        Container
            .Bind<ICharacterBulletConfig>()
            .To<BulletConfig>()
            .FromInstance(_characterBulletConfig)
            .AsCached();
    }

    private void EnemyBulletConfigRegistary()
    {
        Container
            .Bind<IEnemyBulletConfig>()
            .To<BulletConfig>()
            .FromInstance(_enemyBulletConfig)
            .AsCached();
    }

    private void EnemySpawnerConfigRegistary()
    {
        Container
            .Bind<EnemySpawnerConfig>()
            .FromInstance(_enemySpawnerConfig)
            .AsSingle();
    }

    private void EnemyMovementConfigRegistary()
    {
        Container
            .Bind<EnemyMovementConfig>()
            .FromInstance(_enemyMovementConfig)
            .AsSingle();
    }

    private void CharacterConfigRegistary()
    {
        Container
            .Bind<CharacterConfig>()
            .FromInstance(_characterConfig)
            .AsSingle();
    }
}