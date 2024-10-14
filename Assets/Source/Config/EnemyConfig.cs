using UnityEngine;

namespace ShootEmUp.Model
{
    [CreateAssetMenu(menuName = "Conffig/DefoltEnemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _maxHealth;

        public int MaxHealth => _maxHealth;
    }
}