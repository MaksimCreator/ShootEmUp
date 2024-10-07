using UnityEngine;

namespace ShootEmUp.Model
{
    [CreateAssetMenu(menuName = "Conffig/BulletConfig")]
    public sealed class BulletConfig : ScriptableObject,IEnemyBulletConfig,ICharacterBulletConfig
    {
        [SerializeField] private LayerBullet _layer;
        [SerializeField] private Color _color;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public Color Color => _color;
        public int Damage => _damage;
        public float Speed => _speed;
        public int Layer => (int)_layer;

    }
}