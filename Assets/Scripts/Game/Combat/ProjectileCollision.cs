using Game.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private GameObject explosionObject;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Vision"))
        {
            var point = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            Instantiate(explosionObject, point, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
