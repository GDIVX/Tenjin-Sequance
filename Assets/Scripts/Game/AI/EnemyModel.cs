using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI
{
    [CreateAssetMenu(fileName = "Enemy Model", menuName = "Game/Entities", order = 0)]
    public class EnemyModel : ScriptableObject, IEnemyModel
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int health;
        [SerializeField] private float speed;
        [SerializeField, BoxGroup("Attack")] private int damage;
        [SerializeField, BoxGroup("Attack")] private float attackWindup;
        [SerializeField, BoxGroup("Attack")] private float attackCooldown;
        [SerializeField, BoxGroup("Attack")] private float attackRange;

        public GameObject Prefab => prefab;

        public int Health => health;

        public int Damage => damage;

        public float Speed => speed;

        public float AttackWindup => attackWindup;

        public float AttackCooldown => attackCooldown;

        public float AttackRange => attackRange;
    }
}