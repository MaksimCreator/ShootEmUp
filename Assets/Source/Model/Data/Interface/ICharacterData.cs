using UnityEngine;

namespace ShootEmUp.Model
{
    public interface ICharacterData : IService
    {
        int MaxHealth { get; }
        public int MinHealth { get; }
        public float CurentHealth { get; }
    }
}
