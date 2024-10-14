using UnityEngine;

[CreateAssetMenu(menuName = "Conffig/Character")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _speed;
    [SerializeField] private float _cooldown;

    public int Speed => _speed;
    public int MaxHealth => _maxHealth;
    public float Cooldown => _cooldown;
}