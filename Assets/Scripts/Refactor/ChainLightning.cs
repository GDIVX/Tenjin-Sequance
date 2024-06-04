using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace Combat.Weapons
{
    public class ChainLightning : MonoBehaviour
    {
        [SerializeField, BoxGroup("Jump")] private float delayPerJump;
        [SerializeField, BoxGroup("Jump")] private uint baseMaxJumps;
        [SerializeField, BoxGroup("Jump")] private float windUpTime;

        [SerializeField, BoxGroup("Damage")] private int baseDamage;

        [SerializeField, BoxGroup("Targeting")] private LayerMask targetMask;
        [SerializeField, BoxGroup("Targeting")] float maxDistancePerJump = 5;

        [SerializeField, BoxGroup("Effect")] Transform p4;
        [SerializeField, BoxGroup("Effect")] Transform p1;
        [SerializeField, BoxGroup("Effect")] VisualEffect bolt;

        [SerializeField, TabGroup("Debug")] bool debugMode;

        public UnityEvent<Collider> OnHit;
        public UnityEvent OnMarbleAttack;
        public UnityEvent OnMarbleAttackEnd;

        bool IsAttacking = false;
        float cooldown = .3f;

        Collider lastHitTarget;
        private int Damage => baseDamage;
        private uint MaxJumps => baseMaxJumps;

        private void Start()
        {
            UpdateWeapon();
        }


        public void UpdateWeapon()
        {
            if (!IsAttacking)
            {
                StartCoroutine(WeaponWindUp());
            }
        }

        protected IEnumerator WeaponWindUp()
        {
            OnMarbleAttack?.Invoke();
            yield return new WaitForSeconds(windUpTime);
            StartCoroutine(WeaponTrigger());
        }

        protected IEnumerator WeaponTrigger()
        {
            OnMarbleAttackEnd?.Invoke();
            IsAttacking = true;
            int jumps = 0;
            Vector3 currPosition = transform.position;
            while (jumps < MaxJumps)
            {
                float currJumpDistance = maxDistancePerJump;
                Collider[] hits = Physics.OverlapSphere(currPosition, currJumpDistance, targetMask);

                if (hits.Length > 0)
                {
                    Collider closestTarget = FindClosestTarget(hits, currPosition);
                    if (closestTarget != null && closestTarget.transform.TryGetComponent(out IDamageable damageable)) 
                    {
                        lastHitTarget = closestTarget;
                        //DebugDraw(currPosition, closestTarget.transform.position);
                        bolt.transform.position = currPosition;
                        p1.position = currPosition;
                        p4.position = closestTarget.transform.position;
                        bolt.gameObject.SetActive(true);
                        damageable.HandleDamage(Damage);
                        yield return new WaitForSeconds(delayPerJump);
                        currPosition = closestTarget.transform.position;

                        OnHit?.Invoke(closestTarget);
                        bolt.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Fallback if no valid damageable target is found
                        break;
                    }
                }
                else
                {
                    break;
                }

                // Increment jumps counter to ensure loop progresses
                jumps++;
            }

            yield return new WaitForSeconds(cooldown);
            IsAttacking = false;
        }


        private Collider FindClosestTarget(Collider[] targets, Vector3 currentPos)
        {
            Collider closest = null;
            float minDistance = float.MaxValue;
            foreach (Collider target in targets)
            {
                float distance = Vector3.Distance(currentPos, target.transform.position);
                if (distance == 0) continue;
                if (distance < minDistance)
                {
                    closest = target;
                    minDistance = distance;
                }
            }

            return closest;
        }

        public void OnHitDebug()
        {
            Debug.Log("hit enemy");
        }

        private void DebugDraw(Vector3 start, Vector3 end)
        {
            if (debugMode)
            {
                Debug.DrawLine(start, end, Color.blue, cooldown + delayPerJump);
                Debug.Log($"Lightning from {start} to {end}");
            }
        }
    }
}