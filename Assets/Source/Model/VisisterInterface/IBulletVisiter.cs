namespace ShootEmUp.Model
{
    public interface IBulletVisiter
    {
        void Visit(CharacterBullet bullet);
        void Visit(EnemyBullet bullet);
    }
}
