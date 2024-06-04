using System;
using System.Collections;
using AI;
using Game.AI;
using Game.Combat;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Combat
{
    [RequireComponent(typeof(Collider))]
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField, TabGroup("Settings")] private int damage;
        [SerializeField, TabGroup("Settings")] private float attackCooldown;
        [SerializeField, TabGroup("Settings")] private float attackWindup;
        [SerializeField, TabGroup("Settings")] private float range;

        [SerializeField, TabGroup("Settings")] private EnemyTargeting targeting;

        //Animation Events

        [SerializeField, TabGroup("Events")] private UnityEvent OnAttackWindup;
        [SerializeField, TabGroup("Events")] private UnityEvent OnAttacking;
        [SerializeField, TabGroup("Events")] private UnityEvent OnAttackOnCooldownEnd;

        private ITarget target;
        private bool isAttacking = false;

        Animator anim;

        public void Init(int inDamage, float inAttackWindup, float inAttackCooldown, float inRange)
        {
            damage = inDamage;
            attackCooldown = inAttackCooldown;
            attackWindup = inAttackWindup;
            range = inRange;
        }

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();   
        }

        private void Update()
        {
            if (isAttacking) return;

            if (target == null)
            {
                target = targeting.GetTarget();
                return;
            }

            var position = transform.position;
            var distance = Vector3.Distance(position, target.Position);

            if (distance <= range)
            {
                StartCoroutine(Attack(target.Damageable));
            }
        }

        IEnumerator Attack(IDamageable damageable)
        {
            isAttacking = true;
            //anim.SetTrigger("AttackTrigger");
            //windup
            OnAttackWindup?.Invoke();
            yield return new WaitForSeconds(attackWindup);

            //the actual attack

            OnAttacking?.Invoke();
            damageable.HandleDamage(damage);

            //cooldwon
            yield return new WaitForSeconds(attackCooldown);
            OnAttackOnCooldownEnd?.Invoke();

            isAttacking = false;
        }

        public void SetAttackingAllowed(bool value)
        {
            isAttacking = value;
        }
    }
}