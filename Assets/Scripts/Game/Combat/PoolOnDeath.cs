using System;
using AI;
using Game.Combat;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
    public class PoolOnDeath : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] Enemy enemy;
        private EnemySpawnManager spawnManager;


        private void Awake()
        {
            enemy ??= GetComponent<Enemy>();
            health ??= GetComponent<Health>();

            //spawnManager = GameObject.Find("Wave Spawner").GetComponent<EnemySpawnManager>();
            
            //TODO refactor
            gameObject.SetActive(false);

            health.OnDestroyed += x => { spawnManager.ReturnToPool(enemy); };

        }
    }
}