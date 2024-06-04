using System;
using System.Collections;
using Combat;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class Health : MonoBehaviour, IDamageable, IHealable, ITarget
    {
        [SerializeField] private float maxHealth;
        [ShowInInspector, ReadOnly] private float currentHealth;
        [SerializeField] private bool canBeDamaged = true;
        [SerializeField] private float deathTime;
        public event Action<float, IDamageable> OnUpdateValue;

        public UnityEvent OnDeathUnityEvent;
        public UnityEvent OnTakeDamage;
        public UnityEvent<float, Vector3> OnTakeDamageUI;

        public GameObject GameObject => gameObject;
        public IDamageable Damageable => this;

        public bool CanBeDamaged
        {
            get => canBeDamaged;
            set => canBeDamaged = value;
        }

        public void Init(int modelHealth)
        {
            maxHealth = modelHealth;
            currentHealth = maxHealth;
        }

        public event Action<IDestroyable> OnDestroyed;
        public Vector3 Position => transform.localPosition;

        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;

        private void Start()
        {
            currentHealth = maxHealth;
            OnUpdateValue?.Invoke(currentHealth, this);
        }

        [Button]
        public void HandleDamage(float damage)
        {
            if (!CanBeDamaged) return;


            //can we take this hit?
            if (damage >= CurrentHealth)
            {
                OnDestroyed?.Invoke(this);
                OnDeathUnityEvent?.Invoke();
                return;
            }

            currentHealth = CurrentHealth - damage;
            OnUpdateValue?.Invoke(damage, this);
            OnTakeDamage?.Invoke();
            OnTakeDamageUI?.Invoke(damage, transform.position);
        }

        [Button]
        public void Heal(float amount)
        {
            currentHealth = Mathf.Clamp(CurrentHealth + amount, CurrentHealth, MaxHealth);
            OnUpdateValue?.Invoke(amount, this);
        }

        [Button]
        public void MaxHeal()
        {
            Heal(MaxHealth);
        }

        public GameObject TargetGO()
        {
            return this.gameObject;
        }

        public void KillEntity()
        {
            StartCoroutine(KillAfterSeconds());
        }

        private IEnumerator KillAfterSeconds()
        {
            yield return new WaitForSeconds(deathTime);
            gameObject.SetActive(false);
        }
    }
}