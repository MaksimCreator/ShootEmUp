using UnityEngine;
using ShootEmUp.Model;

namespace ShootEmUp.View
{

    public class EntityViewFactory : TransformableViewFactory<Entity>
    {
        [SerializeField] private TransformableView _enemy;
        [SerializeField] private TransformableView _character;

        private EntityVisiter _visiter;

        private void Awake()
        => _visiter = new EntityVisiter(_enemy, _character);

        protected override TransformableView GetTemplate(Entity entity)
        {
            _visiter.Visit((dynamic)entity);
            return _visiter.Prefab;
        }

        private class EntityVisiter : IEntityVisiter
        {
            private readonly TransformableView _enemy;
            private readonly TransformableView _character;

            public TransformableView Prefab { get; private set; }

            public EntityVisiter(TransformableView enemy, TransformableView character)
            {
                _enemy = enemy;
                _character = character;
            }

            public void Visit(Character character)
            => Prefab = _character;

            public void Visit(DefoltEnemy enemy)
            => Prefab = _enemy;
        }
    }
}