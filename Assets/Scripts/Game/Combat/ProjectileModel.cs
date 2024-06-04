using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "ProjectileModel", menuName = "Game/Combat/Projectile", order = 0)]
    public class ProjectileModel : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField, AssetsOnly] private GameObject prefab;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private float lifetime;

        public float Speed => speed;
        public GameObject Prefab => prefab;
        public float Damage => damage;
        public float Lifetime => lifetime;
        public LayerMask TargetLayerMask => targetLayerMask;
    }
}