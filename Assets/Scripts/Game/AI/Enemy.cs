using Combat;
using UnityEngine;
using Utility;

namespace AI
{
    public class Enemy : MonoBehaviour, IPoolable
    {
        public bool IsActive { get; set; }

        public IDamageable Damageable { get; private set; }
        public EnemyMovement Movement { get; private set; }

        public Enemy Init(IDamageable component, EnemyMovement movement)
        {
            Damageable = component;
            Movement = movement;

            return this;
        }
    }
}