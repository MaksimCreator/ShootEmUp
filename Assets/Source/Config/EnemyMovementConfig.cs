using UnityEngine;

namespace ShootEmUp.Model
{
    [CreateAssetMenu(menuName = "Conffig/EnemyMovement")]
    public class EnemyMovementConfig : ScriptableObject
    {
        [SerializeField] private float _cooldownIncrement;
        [SerializeField] private float _speed;
        [SerializeField] private float _speedIncrement;
        [SerializeField] private float _maxSpeed;
        
        public float CooldownIncrement => _cooldownIncrement;
        public float Speed => _speed;
        public float SpeedIncrement => _speedIncrement;
        public float MaxSpeed => _maxSpeed;
    }
}