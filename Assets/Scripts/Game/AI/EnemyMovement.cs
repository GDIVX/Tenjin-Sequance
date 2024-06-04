using System;
using Combat;
using Game.AI;
using Game.Combat;
using UnityEngine;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;
using UnityEngine.AI;
using UnityEngine.Events;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField, TabGroup("Setting")] private EnemyTargeting targeting;
        [SerializeField, TabGroup("Setting")] private NavMeshAgent navMeshAgent;

        [SerializeField, TabGroup("View")] private Transform view;
        [SerializeField, TabGroup("View")] private float rotationSpeed;

        [SerializeField, TabGroup("Setting")] private float speed;
        private Animator anim;

        private bool isMoving = true;

        [SerializeField, TabGroup("Events")] private UnityEvent<string, bool> OnEnemyMove;
        [SerializeField, TabGroup("Events")] private UnityEvent<string, bool> OnEnemyMoveEnd;


        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
        }

        public void SetMovementAllowed(bool value)
        {
            isMoving = value;
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = !value;
            }
        }

        private void Start()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.speed = speed;
            }
        }

        private void Update()
        {
            if (!isMoving)
            {
                OnEnemyMoveEnd?.Invoke("isRunning", false);
                return;
            }

            OnEnemyMove?.Invoke("isRunning", true);

            ITarget target = targeting.GetTarget();
            Vector3 destination;

            if (target == null)
            {
                var circle = Random.insideUnitCircle * 5f; // Move within a radius of 5 units
                destination = transform.position + new Vector3(circle.x, 0, circle.y);
            }
            else
            {
                destination = target.Position;
            }

            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(destination);
            }

            // Handle rotation
            if (navMeshAgent != null && navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Quaternion rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
                view.rotation = Quaternion.Lerp(view.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }

        public void Init(float modelSpeed)
        {
            speed = modelSpeed;
            if (navMeshAgent != null)
            {
                navMeshAgent.speed = speed;
                navMeshAgent.isStopped = false;
            }

            isMoving = true;
        }

        private void OnValidate()
        {
            // Ensure necessary components are present
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();

            // Validate serialized fields to avoid null reference issues
            if (targeting == null)
            {
                Debug.LogError("EnemyTargeting component is not assigned.", this);
            }

            if (view == null)
            {
                Debug.LogError("View Transform is not assigned.", this);
            }

            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component is missing.", this);
            }

            if (anim == null)
            {
                Debug.LogError("Animator component is missing in children.", this);
            }
        }
    }
}