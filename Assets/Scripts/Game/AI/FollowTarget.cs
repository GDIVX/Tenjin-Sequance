using UnityEngine;
using UnityEngine.AI;

namespace Game.AI
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private string tagToFind;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float stoppingDistance;

        private GameObject target;

        private void Start()
        {
            target = GameObject.FindWithTag(tagToFind);
        }

        private void Update()
        {
            if (!target)
            {
                target = GameObject.FindWithTag(tagToFind);
                return;
            }

            if (agent.hasPath) return;

            if (Vector3.Distance(target.transform.position, transform.position) <= stoppingDistance)
            {
                return;
            }

            var targetPos = target.transform.position;
            var direction = (targetPos - transform.position).normalized;
            var stoppingVector = direction * stoppingDistance;
            var destination = targetPos - stoppingVector;


            agent.SetDestination(destination);
        }
    }
}