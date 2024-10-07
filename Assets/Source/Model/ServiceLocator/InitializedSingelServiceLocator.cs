using ShootEmUp.Model;

public static class InitializedSingelServiceLocator
{
    private static bool _isInitialized;

    public static void TryInitialized(CharacterConfig characterConfig, BulletConfig characterBulletConfig,BulletConfig enemyBulletConfig, EnemySpawnerConfig enemySpawnerConffig, EnemyMovementConfig enemyMovementConfig)
    {
        if (_isInitialized)
            return;

        _isInitialized = true;
        Initialized(characterConfig,characterBulletConfig,enemyBulletConfig,enemySpawnerConffig,enemyMovementConfig);
    }

    private static void Initialized(CharacterConfig characterConfig, BulletConfig bulletConfig, BulletConfig enemyBulletConfig, EnemySpawnerConfig enemySpawnerConffig, EnemyMovementConfig enemyMovementConfig)
    {
        DataSave dataSave = new DataSave();
        Wallet wallet = new Wallet();

        ConfigRegistary(characterConfig,bulletConfig, enemyBulletConfig, enemySpawnerConffig,enemyMovementConfig);
        DataServiceRegistary(dataSave);
        DataRegistary(new SingelPlayerData(wallet));
        WalletRegistary(wallet);
    }

    private static void ConfigRegistary(CharacterConfig characterConfig,BulletConfig characterBulletConfig,BulletConfig enemyBulletConfig,EnemySpawnerConfig enemySpawnerConffig,EnemyMovementConfig enemyMovementConfig)
    {
        SingelServiceLocator.RegisatryService<ICharacterBulletConfig>(characterBulletConfig);
        SingelServiceLocator.RegisatryService<IEnemyBulletConfig>(enemyBulletConfig);
        SingelServiceLocator.RegisatryService(enemyMovementConfig);
        SingelServiceLocator.RegisatryService(enemySpawnerConffig);
        SingelServiceLocator.RegisatryService(characterConfig);
    }

    private static void DataRegistary(SingelPlayerData data)
    { 
        SingelServiceLocator.RegisatryService(data);
        SingelServiceLocator.RegisatryService(SingelServiceLocator.GetService<IDataService>().Load());
    }

    private static void DataServiceRegistary(DataSave dataService)
    => SingelServiceLocator.RegisatryService<IDataService>(dataService);

    private static void WalletRegistary(Wallet wallet)
    => SingelServiceLocator.RegisatryService<IWallet>(wallet);
}
