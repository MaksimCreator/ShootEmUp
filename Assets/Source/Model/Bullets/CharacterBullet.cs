using UnityEngine;

namespace ShootEmUp.Model
{
    public class CharacterBullet : Bullet
    {
        public CharacterBullet(Vector2 startPosition,ICharacterBulletConfig config) : base(startPosition,config.Damage, config.Speed, config.Color) { }
    }
}
