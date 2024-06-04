using System;
using System.Collections;
using Combat;
using Game.Combat;
using ModestTree;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Queue
{
    [RequireComponent(typeof(ProjectileFactory))]
    public class MarbleShooter : MonoBehaviour
    {
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private MarbleQueue _queue;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private float marbleShotTime;
        [SerializeField] private bool isShooting;


        [Tooltip("Triggered when attempting to shoot a marble. Rerun true if it was successful")]
        public UnityEvent<bool> onShootingMarbleAttempted;
        public UnityEvent onShootingMarble;
        public UnityEvent onShootingMarbleEnd;

        private void Awake()
        {
            _projectileFactory ??= GetComponent<ProjectileFactory>();
            _queue ??= GetComponent<MarbleQueue>();
        }

        public void ShootNextMarble()
        {
            if (isShooting) return;

            //If the queue is empty, throw an event for the UI/UX and return
            if (_queue.IsEmpty())
            {
                onShootingMarbleAttempted?.Invoke((false));
                return;
            }

            //Try to fetch the next marble from the queue
            Marble marble = _queue.EjectMarble();

            if (marble == null)
            {
                Debug.LogError("Failed to fetch marble from a non empty queue");
                onShootingMarbleAttempted?.Invoke((false));
                return;
            }


            //Instantiate a projectile

            Projectile projectile = _projectileFactory.Create(marble.ProjectileModel, spawnPoint.position);

            //rotate the projectile
            projectile.transform.rotation = Quaternion.LookRotation(transform.forward);

            //trigger event for sucssus
            onShootingMarbleAttempted?.Invoke(true);
            onShootingMarble.Invoke();
            isShooting = true;
            StartCoroutine(MarbleShotCoroutine());

        }

        protected IEnumerator MarbleShotCoroutine()
        {
            yield return new WaitForSeconds(marbleShotTime);
            isShooting = false;
            onShootingMarbleEnd?.Invoke();
        }
    }
}