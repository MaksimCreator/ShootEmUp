using Zenject;
using UnityEngine;
using ShootEmUp.View;
using ShootEmUp.Model;
using ShootEmUp.Observer;
using ShootEmUp.Controller;
using System.Collections.Generic;
using CharacterController = ShootEmUp.Controller.CharacterController;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private BulletViewFactroy _bulletFactory;
    [SerializeField] private EntityViewFactory _entityViewFactory;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Transform _restrictionsLeft;
    [SerializeField] private Transform _restrictionsRight;
    [SerializeField] private List<Transform> _enemyPointsSpawn = new();
    [SerializeField] private CharacterConfig _characterConfig;

    public override void InstallBindings()
    {
        RegistaryPhysics();
        RegistaryFactory();
        RegistaryInput();
        RegistaryCharacter();
        RegistaryService();
        RegistaryMovement();
        RegistarySimulated();
        RegistaryData();
        RegistaryObserver();
        RegistarySpawner();
        RegistaryCreaters();
        RegistaryController();
        RegistaryGamaplayFsm();
    }

    private void RegistaryCreaters()
    {
        Container
            .Bind<IEntityCreater>()
            .To<EntityCreater>()
            .FromNew()
            .AsSingle();
    }

    #region RegistaryGamaplayFsm

    private void RegistaryGamaplayFsm()
    {
        Fsm fsm = new Fsm();

        Container
           .Bind<IGameFsm>()
           .To<Fsm>()
           .FromInstance(fsm)
           .AsSingle();

        RegistaryGamaplayState(fsm);
    }


    #region RegistaryGamaplayState

    private void RegistaryGamaplayState(IGameFsm fsm)
    {
        RegistaryLevelInitializedState();
        RegistaryGUIInitializedState();
        RegistaryPhysicsRoutingState();
        RegistaryIdelState();
    }

    private void RegistaryLevelInitializedState()
    {
        Container
            .Bind<LevelInitializedState>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryIdelState()
    {
        Container
           .Bind<IdelState>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryGUIInitializedState()
    {
        Container
           .Bind<GUIInitializedState>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryPhysicsRoutingState()
    {
        Container
           .Bind<PhysicsRoutingState>()
           .FromNew()
           .AsSingle();
    }

    #endregion

    #endregion

    #region RegistaryController

    private void RegistaryController()
    {
        RegistaryBulletController();
        RegistaryCharacterController();
        RegistaryEnemysController();
        RegistaryObserverController();
        RegistaryGameController();
    }

    private void RegistaryGameController()
    {
        Container
           .Bind<GameController>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryCharacterController()
    {
        Container
           .Bind<CharacterController>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryBulletController()
    {
        Container
          .Bind<BulletController>()
          .FromNew()
          .AsSingle();
    }

    private void RegistaryEnemysController()
    {
        Container
          .Bind<EnemysController>()
          .FromNew()
          .AsSingle();
    }

    private void RegistaryObserverController()
    {
        Container
          .Bind<ObserverController>()
          .FromNew()
          .AsSingle();
    }

    #endregion

    #region RegistaryService

    private void RegistaryService()
    {
        RegistaryEnemyShot();
        RegistaryEnemyPoints();
        RegistaryCharacterRestrictions();
    }

    private void RegistaryEnemyShot()
    {
        Container
          .Bind<IEnemyShot>()
          .To<EnemyShot>()
          .FromNew()
          .AsSingle();
    }

    private void RegistaryEnemyPoints()
    {
        EnemySpawnPoint enemySpawnPoint = new EnemySpawnPoint(_enemyPointsSpawn.ToArray());

        Container
          .Bind<IEnemySpawnPoint>()
          .To<EnemySpawnPoint>()
          .FromInstance(enemySpawnPoint)
          .AsSingle();
    }

    private void RegistaryCharacterRestrictions()
    {
        CharacterRestrictions characterRestrictions = new CharacterRestrictions(_restrictionsLeft, _restrictionsRight);

        Container
         .Bind<ICharacterRestrictions>()
         .To<CharacterRestrictions>()
         .FromInstance(characterRestrictions)
         .AsSingle();
    }

    #endregion

    #region RegistarySpawner

    private void RegistarySpawner()
    {
        RegistaryCharacterSpawnerBullet();
        RegistaryEnemySpawnerBullet();
        RegistaryRowSpawner();
    }

    private void RegistaryRowSpawner()
    {
        Container
           .Bind<RowSpawner>()
           .AsSingle();
    }

    private void RegistaryCharacterSpawnerBullet()
    {
        Container
            .Bind<CharacterSpawnerBullet>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryEnemySpawnerBullet()
    {
        Container
            .Bind<EnemySpawnerBullet>()
            .FromNew()
            .AsSingle();
    }

    #endregion

    #region RegistarySimulated

    private void RegistarySimulated()
    {
        RegistaryBulletSimulated();
        RegistaryRowSimulated();
    }

    private void RegistaryRowSimulated()
    {
        Container
          .Bind<RowSimulated>()
          .ToSelf()
          .AsSingle();
    }

    private void RegistaryBulletSimulated()
    {
        Container
         .Bind<IBulletSimulated>()
         .To<BulletSimulated>()
         .AsSingle();
    }

    #endregion

    #region RegistaryMovement

    private void RegistaryMovement()
    {
        RegistaryCharacterMovement();
        RegistaryEnemyMovement();
        RegistaryBulletMovement();
        RegistaryRowMovement();
    }

    private void RegistaryCharacterMovement()
    {
        Container
           .Bind<ICharacterMovement>()
           .To<CharacterMovement>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryEnemyMovement()
    {
        Container
           .Bind<IEnemyMovement>()
           .To<EnemyMovement>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryRowMovement()
    {
        Container
          .Bind<RowMovement>()
          .FromNew()
          .AsSingle();
    }

    private void RegistaryBulletMovement()
    {
        Container
          .Bind<BulletMovement>()
          .FromNew()
          .AsSingle();
    }

    #endregion

    #region RegistaryPhysics

    private void RegistaryPhysics()
    {
        CollisionRecord collisionRecord = new CollisionRecord();
        PhysicsRouter physicsRouter = new PhysicsRouter(collisionRecord.Values);

        Container
            .Bind<IPhysicsRouter>()
            .To<PhysicsRouter>()
            .FromInstance(physicsRouter)
            .AsSingle();
    }

    #endregion

    #region RegistaryFactory

    private void RegistaryFactory()
    {
        RegistaryBulletFactory();
        RegistaryEntityFactory();
    }

    private void RegistaryBulletFactory()
    {
        Container
            .Bind<BulletViewFactroy>()
            .FromInstance(_bulletFactory)
            .AsSingle();
    }

    private void RegistaryEntityFactory()
    {
        Container
            .Bind<EntityViewFactory>()
            .FromInstance(_entityViewFactory)
            .AsSingle();
    }

    #endregion

    #region RegistaryInput

    private void RegistaryInput()
    {
        InputRouter inputRouter = new InputRouter();

        RegistaryIInputControl(inputRouter);
        RegistaryIInputService(inputRouter);
    }

    private void RegistaryIInputControl(InputRouter inputRouter)
    {
        Container
            .Bind<IInputControl>()
            .To<InputRouter>()
            .FromInstance(inputRouter)
            .AsCached();
    }

    private void RegistaryIInputService(InputRouter inputRouter)
    {
        Container
            .Bind<IInputService>()
            .To<InputRouter>()
            .FromInstance(inputRouter)
            .AsCached();
    }

    #endregion

    #region RegistaryCharacter

    private void RegistaryCharacter()
    {
        PlayerHealth playerHealth = new PlayerHealth(_characterConfig);
        RegistaryCharacterHealth(playerHealth);

        Character character = new Character(playerHealth, _characterTransform.position);

        Container
            .Bind<Character>()
            .FromInstance(character)
            .AsSingle();
    }

    private void RegistaryCharacterHealth(PlayerHealth playerHealth)
    {
        Container
            .Bind<PlayerHealth>()
            .FromInstance(playerHealth)
            .AsSingle();
    }

    #endregion

    #region RegistaryData

    private void RegistaryData()
    {
        Container
            .Bind<PlayerData>()
            .FromNew()
            .AsSingle();
    }

    #endregion

    #region RegistaryObserver

    private void RegistaryObserver()
    {
        RegistaryAction();

        RegistaryCharacterBulletObserver();
        RegistaryEnemyBulletObserver();

        RegistaryCharacterObserver();
        RegistaryEnemyObserver();
        RegistaryRowObserver();
    }

    #region Observer

    private void RegistaryCharacterBulletObserver()
    {
        Container
           .Bind<ICharacterBulletObserver>()
           .To<BulletObserver>()
           .FromNew()
           .AsCached();
    }

    private void RegistaryEnemyBulletObserver()
    {
        Container
           .Bind<IEnemyBulletObserver>()
           .To<BulletObserver>()
           .FromNew()
           .AsCached();
    }

    private void RegistaryCharacterObserver()
    {
        Container
           .Bind<ICharacterObserver>()
           .To<CharacterObserver>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryEnemyObserver()
    {
        Container
           .Bind<IEnemyObserver>()
           .To<EnemyObserver>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryRowObserver()
    {
        Container
           .Bind<IRowObserver>()
           .To<RowObserver>()
           .FromNew()
           .AsSingle();
    }

    #endregion

    #region Action

    private void RegistaryAction()
    {
        RegistaryBulletDisableAction();

        RegistaryCharacterDeathAction();
        RegistaryCharacterTakeDamageAction();

        RegistaryEnemyDeathAction();
        RegistaryEnemyDisableAction();
        RegistaryEnemyDisableFromZone();

        RegistaryRowEndAction();

        RegistaryUpdateWalletAction();
    }

    private void RegistaryBulletDisableAction()
    {
        Container
            .Bind<BulletDisableAction>()
            .FromNew()
            .AsSingle();
    }

    private void RegistaryCharacterDeathAction()
    {
        Container
           .Bind<CharacterDeathAction>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryCharacterTakeDamageAction()
    {
        Container
           .Bind<CharacterTakeDamageAction>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryEnemyDeathAction()
    {
        Container
           .Bind<EnemyDeathAction>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryEnemyDisableFromZone()
    {
        Container
           .Bind<EnemyDisableFromZone>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryEnemyDisableAction()
    {
        Container
           .Bind<EnemyDisableAction>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryRowEndAction()
    {
        Container
           .Bind<RowEndAction>()
           .FromNew()
           .AsSingle();
    }

    private void RegistaryUpdateWalletAction()
    {
        Container
           .Bind<UpdateWalletAction>()
           .FromNew()
           .AsSingle();
    }

    #endregion

    #endregion
}