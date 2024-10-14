using ShootEmUp.View;
using System;
using Zenject;

namespace ShootEmUp.Model
{
    public sealed class EnemysController : IControl, IDeltaUpdatable
    {
        private readonly EntityViewFactory _entityViewFactory;
        private readonly RowSpawner _enemySpawner;
        private readonly IEnemyShot _enemyShot;

        private readonly IDeltaUpdatable[] _deltaUpdatableEnemy;
        private readonly IControl[] _controlEnemy;

        [Inject]
        public EnemysController(EntityViewFactory entityViewFactory,RowSpawner enemySpawner,IEnemyShot enemyShot,RowSimulated rowSimulated,IEnemyMovement enemyMovement)
        {
            _entityViewFactory = entityViewFactory;
            _enemySpawner = enemySpawner;
            _enemyShot = enemyShot;

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
            _enemySpawner.onCreat -= _entityViewFactory.Create;

            for (int i = 0; i < _controlEnemy.Length; i++)
                _controlEnemy[i].Disable();
        }

        public void Enable()
        {
            _enemySpawner.onCreat += _entityViewFactory.Create;

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
