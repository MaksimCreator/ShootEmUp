using UnityEngine;

namespace ShootEmUp.Model
{
    public class EnemyBullet : Bullet
    {
        public EnemyBullet(Vector2 startPosition, IEnemyBulletConfig config) : base(startPosition, config.Damage, config.Speed, config.Color) { }
    }
}
