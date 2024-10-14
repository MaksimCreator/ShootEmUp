using UnityEngine;

namespace ShootEmUp.Model
{
    [CreateAssetMenu(menuName = "Conffig/EnemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private float _cooldownSpawn;

        public EnemyConfig EnemyConfig => _enemyConfig;
        public float CooldownSpawn => _cooldownSpawn;
    }
}