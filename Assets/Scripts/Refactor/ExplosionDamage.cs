using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] private int explosionDamageAmount;
    private List<IDamageable> EntityInRange;

    public int ExplosionDamageAmount
    {
        get => explosionDamageAmount;
        private set => explosionDamageAmount = value;
    }

    private void Awake()
    {
        EntityInRange = new List<IDamageable>();
    }

    public void DoDamage()
    {
        Debug.Log("Attacking");
        foreach (IDamageable target in EntityInRange)
        {
            target.HandleDamage(ExplosionDamageAmount);
            Debug.Log("!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        if (other.TryGetComponent(out IDamageable target))
        {
            EntityInRange.Add(target);
            DoDamage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;

        if (other.TryGetComponent(out IDamageable target))
        {
            EntityInRange.Remove(target);
        }
    }
}
