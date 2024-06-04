using Game.Combat;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedbackUI : MonoBehaviour
{
    [SerializeField] float amountToPool;
    [SerializeField] GameObject damageNumberPrefab;
    [SerializeField] List<GameObject> damageNumbers = new List<GameObject>();

    private void Start()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            var go = Instantiate(damageNumberPrefab, transform);
            damageNumbers.Add(go);
            if (go.TryGetComponent(out DamageNumber damageNumber) && !damageNumber.isAvailable) damageNumber.isAvailable = true;
            go.gameObject.SetActive(false);
        }
    }

    public void GetDamageNumber(float num, Vector3 pos)
    {
        foreach (var item in damageNumbers)
        {
            if (item.TryGetComponent(out DamageNumber damageNumber))
            {
                if (damageNumber.isAvailable)
                {
                    item.gameObject.SetActive(true);
                    damageNumber.ActivateDamageNumber(num, pos);
                    break;
                }
            }
            else Debug.Log("cant get component damagenumber");
        }

        if (damageNumbers.Count == 0)
        {
            Debug.Log("no available damage number found, try increasing the pool amount.");
        }
    }
}
