using System.Collections.Generic;
using static ShootEmUp.Model.PhysicsRouter;

namespace ShootEmUp.Model
{
    public class CollisionRecord
    {
        public IEnumerable<Record> Values()
        {
            yield return new Record<Enemy, Character>((enemy, character) => character.Death());
            yield return new Record<Bullet, DisableBulletZone>((bullet,disableBulletZone) => bullet.Disable());
            yield return new Record<Enemy, DisableEnemyZone>((enemy, disableEnemyZone) => enemy.DisableFromZone());
            yield return new Record<Bullet, Entity>((bullet, entity) =>
            {
                bullet.Disable();
                entity.TakeDamage(bullet.Damage);
            });
            yield return new Record<Bullet, Bullet>((firstBullet, secondBullet) =>
            {
                firstBullet.Disable();
                secondBullet.Disable();
            });
        }
    }
}
