using UnityEngine;

namespace AI
{
    public interface IEnemyModel
    {
        public GameObject Prefab { get; }
        public int Health { get; }
        public int Damage { get; }
        public float Speed { get; }
        public float AttackWindup { get; }
        public float AttackCooldown { get; }
        public float AttackRange { get; }
    }
}