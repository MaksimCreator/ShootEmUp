using UnityEngine;
using ShootEmUp.Model;
using ShootEmUp.View;
using ShootEmUp.Controller;
using System.Collections.Generic;
using ShootEmUp.Observer;
using CharacterController = ShootEmUp.Controller.CharacterController;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private PhysicsEventsBroadcaster _characterEventBroadcaster;
    [SerializeField] private EntityViewFactory _entityViewFactory;
    [SerializeField] private BulletViewFactroy _bulletViewFactroy;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private List<Transform> _enemyPointsSpawn;
    [SerializeField] private GamaplayMenager _gamaplayMenager;
    [SerializeField] private Transform _restrictionsLeft;
    [SerializeField] private Transform _restrictionsRight;

    private void Awake()
    {
        ServiceLocator locator = new ServiceLocator();
        CharacterConfig config = SingelServiceLocator.GetService<CharacterConfig>();

        RegistaryPhysics(locator);
        RegistaryFactory(locator);
        RegistaryInput(locator);
        RegistaryCharacter(locator,config);
        RegistaryData(locator);
        RegistaryObserver(locator);
        RegistaryMovement(locator,config);
        RegistarySimulated(locator);
        RegistarySpawnerBullet(locator, config);
        RegistaryEnemyShot(locator);
        RegistaryEntityCreater(locator);
        RegistaryEnemySpawner(locator);

        CharacterController characterController = new CharacterController(locator);
        BulletController bulletController = new BulletController(locator);
        EnemysController enemysController = new EnemysController(locator);
        ObserverController observerController = new ObserverController(locator);

        _gamaplayMenager.Initialized(_characterEventBroadcaster,locator,characterController,enemysController,bulletController,observerController);
    }

    private void RegistaryData(ServiceLocator locator)
    => locator.RegistareService(new PlayerData(locator));

    private void RegistaryEnemyShot(ServiceLocator locator)
    => locator.RegistareService<IEnemyShot>(new EnemyShot(locator));

    private void RegistarySpawnerBullet(ServiceLocator locator,CharacterConfig config)
    {
        locator.RegistareService(new CharacterSpawnerBullet(_pointSpawn, locator, config.Cooldown));
        locator.RegistareService(new EnemySpawnerBullet(locator));
    }

    private void RegistaryEntityCreater(ServiceLocator locator)
    => locator.RegistareService<IEntityCreater>(new EntityCreater(locator));

    private void RegistaryEnemySpawner(ServiceLocator locator)
    => locator.RegistareService(new EnemySpawner(locator, _enemyPointsSpawn.ToArray()));

    private void RegistarySimulated(ServiceLocator locator)
    {
        locator.RegistareService<IBulletSimulated>(new BulletSimulated());
        locator.RegistareService(new RowSimulated());
    }

    private void RegistaryMovement(ServiceLocator locator,CharacterConfig config)
    {
        CharacterMovement characterMovement = new CharacterMovement(_restrictionsLeft.position, _restrictionsRight.position, config.Speed, locator);
        EnemyMovement enemyMovement = new EnemyMovement();

        locator.RegistareService<ICharacterMovemeng>(characterMovement);
        locator.RegistareService<IEnemyMovement>(enemyMovement);

    }

    private void RegistaryCharacter(ServiceLocator locator,CharacterConfig config)
    {
        PlayerHealth playerHealth = new PlayerHealth(config.MaxHealth, config.MaxHealth);
        Character character = new Character(playerHealth, _characterEventBroadcaster.transform.position);

        locator.RegistareService(character);
        locator.RegistareService(playerHealth);
    }

    private void RegistaryPhysics(ServiceLocator locator)
    {
        CollisionRecord collisionRecord = new CollisionRecord();
        PhysicsRouter physicsRouter = new PhysicsRouter(collisionRecord.Values);

        locator.RegistareService<IPhysicsRouter>(physicsRouter);
    }

    private void RegistaryFactory(ServiceLocator locator)
    {
        _entityViewFactory.Init(locator);
        _bulletViewFactroy.Init(locator);

        locator.RegistareService(_entityViewFactory);
        locator.RegistareService(_bulletViewFactroy);
    }

    private void RegistaryInput(ServiceLocator locator)
    {
        InputRouter router = new InputRouter();

        locator.RegistareService<IInputControl>(router);
        locator.RegistareService<IInputService>(router);
    }

    private void RegistaryActionObserver(ServiceLocator locator)
    {
        locator.RegistareService(new BulletDisableAction());

        locator.RegistareService(new CharacterDeathAction());
        locator.RegistareService(new CharacterTakeDamageAction());
        
        locator.RegistareService(new EnemyDeathAction());
        locator.RegistareService(new EnemyDisableFromZone());
        locator.RegistareService(new EnemyDisableAction());

        locator.RegistareService(new RowEndAction());

        locator.RegistareService(new UpdateWalletAction());
    }

    private void RegistaryObserver(ServiceLocator locator)
    {
        RegistaryActionObserver(locator);

        locator.RegistareService<ICharacterBulletObserver>(new BulletObserver(locator));
        locator.RegistareService<IEnemyBulletObserver>(new BulletObserver(locator));

        locator.RegistareService<ICharacterObserver>(new CharacterObserver(locator));
        locator.RegistareService<IEnemyObserver>(new EnemyObserver(locator));
        locator.RegistareService<IRowObserver>(new RowObserver(locator));
    }
}