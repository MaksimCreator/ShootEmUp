using ShootEmUp.View;
using System;

namespace ShootEmUp.Model
{
    public sealed class EnemysController : IControl, IDeltaUpdatable
    {
        private readonly EntityViewFactory _entityViewFactory;
        private readonly EnemySpawner _enemySpawner;
        private readonly IEnemyShot _enemyShot;

        private readonly IDeltaUpdatable[] _deltaUpdatableEnemy;
        private readonly IControl[] _controlEnemy;

        public EnemysController(ServiceLocator locator)
        {
            _entityViewFactory = locator.GetService<EntityViewFactory>();
            _enemySpawner = locator.GetService<EnemySpawner>();
            _enemyShot = locator.GetService<IEnemyShot>();

            RowSimulated rowSimulated = locator.GetService<RowSimulated>();
            IEnemyMovement enemyMovement = locator.GetService<IEnemyMovement>();

            _deltaUpdatableEnemy = new IDeltaUpdatable[]
            {
                _enemyShot,
                _enemySpawner,
                rowSimulated,
                enemyMovement
            };
            _controlEnemy = new IControl[]
            {
                _enemyShot as IControl,
                _enemySpawner,
                rowSimulated,
            };
        }

        public void Disable()
        {
            _enemySpawner.onSpawn -= _entityViewFactory.Create;

            for (int i = 0; i < _controlEnemy.Length; i++)
                _controlEnemy[i].Disable();
        }

        public void Enable()
        {
            _enemySpawner.onSpawn += _entityViewFactory.Create;

            for (int i = 0; i < _controlEnemy.Length;i++)
                _controlEnemy[i].Enable();
        }

        public void Update(float delta)
        {
            if (delta <= 0)
                throw new InvalidOperationException(nameof(delta));

            for (int i = 0; i < _deltaUpdatableEnemy.Length; i++)
                _deltaUpdatableEnemy[i].Update(delta);
        }
    }
}
