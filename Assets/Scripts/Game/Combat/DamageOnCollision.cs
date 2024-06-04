using System.Collections;
using Combat;
using Game.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    [RequireComponent(typeof(Collider))]
    public sealed class DamageOnCollision : MonoBehaviour, IInit<ProjectileModel>
    {
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float cooldown;
        public UnityEvent<DamageOnCollision, Collider> OnCollision;

        private bool _canDamage = true;

        public void Init(ProjectileModel input)
        {
            _damage = input.Damage;
            _layerMask = input.TargetLayerMask; // Assuming input provides a LayerMask
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleDamage(other);
        }

        private void OnTriggerStay(Collider other)
        {
            HandleDamage(other);
        }

        private void HandleDamage(Collider other)
        {
            if (!_canDamage || (_layerMask.value & (1 << other.gameObject.layer)) == 0)
            {
                return;
            }

            if (other.gameObject.TryGetComponent(out IDamageable hit))
            {
                hit.HandleDamage(_damage);
                if (cooldown > 0)
                {
                    StartCoroutine(CooldownCoroutine());
                }
            }

            OnCollision?.Invoke(this, other);
        }

        IEnumerator CooldownCoroutine()
        {
            _canDamage = false;
            yield return new WaitForSeconds(cooldown);
            _canDamage = true;
        }
    }
}