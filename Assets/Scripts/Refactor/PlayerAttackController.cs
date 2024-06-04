using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] float meleeAttackCooldown;
    Camera MainCamera;
    bool isShooting;

    #region MeleeParams
    [SerializeField] private int meleeDamage;
    private List<IDamageable> EntityInRange;
    bool canUseMelee = true;

    public int MeleeDamage
    {
        get => meleeDamage;
        private set => meleeDamage = value;
    }
    #endregion

    public UnityEvent OnMeleeAttack;
    public UnityEvent OnMeleeAttackEnd;
    public UnityEvent OnPlayerDeath;

    private void Awake()
    {
        EntityInRange = new List<IDamageable>();
    }


    public void InitMeleeAttack()
    {
        if (!canUseMelee) return;
        StartCoroutine(PunchCoroutine());
    }

    IEnumerator PunchCoroutine()
    {
        canUseMelee = false;
        OnMeleeAttack?.Invoke();
        DoDamage();
        yield return new WaitForSeconds(meleeAttackCooldown);
        canUseMelee = true;
        OnMeleeAttackEnd?.Invoke();
    }

    public void DoDamage()
    {
        //Debug.Log("Attacking");
        foreach (IDamageable target in EntityInRange)
        {
            target.HandleDamage(MeleeDamage);
            //Debug.Log("!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        if (other.TryGetComponent(out IDamageable target))
        {
            EntityInRange.Add(target);
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
    
    //temp
    public void PlayerDead()
    {
        OnPlayerDeath?.Invoke();
        StartCoroutine(KillPlayer());
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(3.7f);
        gameObject.SetActive(false);
    }
}
