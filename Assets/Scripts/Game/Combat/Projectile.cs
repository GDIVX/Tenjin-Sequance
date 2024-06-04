using Game.Combat;
using UnityEngine;
using Utility;

namespace Combat
{
    public class Projectile : MonoBehaviour, IPoolable
    {
        public bool IsActive { get; set; }

        public ProjectileMovement Movement { get; private set; }
        public DamageOnCollision DamageOnCollision { get; private set; }

        public void Initialize(ProjectileMovement movement, DamageOnCollision damageOnCollision)
        {
            Movement = movement;
            DamageOnCollision = damageOnCollision;
        }
    }
}