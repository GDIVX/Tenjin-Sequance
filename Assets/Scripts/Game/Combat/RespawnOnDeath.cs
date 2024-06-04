using System;
using System.Collections;
using Game.Combat;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Combat
{
    public class RespawnOnDeath : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] private float respawnDelay;
        [SerializeField] private Vector3 respawnPosition;
        [SerializeField] private float invulnerabilityDuration;

        public UnityEvent OnDespawn;
        public UnityEvent OnRespawn;

        private void Awake()
        {
            health.OnDestroyed += (h => { StartCoroutine(Respawn()); });
        }

        private IEnumerator Respawn()
        {
            health.gameObject.SetActive(false);
            OnDespawn?.Invoke();
            yield return new WaitForSeconds(respawnDelay);

            health.transform.position = respawnPosition;
            health.CanBeDamaged = false;
            health.MaxHeal();
            health.gameObject.SetActive(true);
            OnRespawn?.Invoke();

            yield return new WaitForSeconds(invulnerabilityDuration);
            health.CanBeDamaged = true;
        }
    }
}