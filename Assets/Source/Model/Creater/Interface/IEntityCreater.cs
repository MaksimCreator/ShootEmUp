using UnityEngine;

namespace ShootEmUp.Model
{
    public interface IEntityCreater
    {
        DefoltEnemy CreatDefoltEnemy(Vector2 startPosition, PoolObject<Entity> poolObject);
        void CreatRow(Enemy[] enemys);
    }
}