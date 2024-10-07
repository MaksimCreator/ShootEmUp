using UnityEngine;
using ShootEmUp.Model;

namespace ShootEmUp.View
{

    public class BulletViewFactroy : TransformableViewFactory<Bullet>
    {
        [SerializeField] private TransformableView _characterBullet;
        [SerializeField] private TransformableView _enemyBullet;

        private BulletVisiter _bulletVisiter;

        private void Awake()
        => _bulletVisiter = new BulletVisiter(_characterBullet,_enemyBullet);

        protected override TransformableView GetTemplate(Bullet bullet)
        {
            _bulletVisiter.Visit((dynamic)bullet);
            return _bulletVisiter.Prefab;
        }

        private class BulletVisiter : IBulletVisiter
        {
            private readonly TransformableView _characterBullet;
            private readonly TransformableView _enemyBullet;

            public TransformableView Prefab { get; private set; }

            public BulletVisiter(TransformableView characterBullet, TransformableView enemyBullet)
            {
                _characterBullet = characterBullet;
                _enemyBullet = enemyBullet;
            }

            public void Visit(CharacterBullet bullet)
            => Prefab = _characterBullet;

            public void Visit(EnemyBullet bullet)
            => Prefab = _enemyBullet;
        }
    }
}