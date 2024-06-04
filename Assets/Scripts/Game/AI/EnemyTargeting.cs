using System;
using System.Collections.Generic;
using System.Linq;
using AI;
using Game.Combat;
using UnityEngine;

namespace Game.AI
{
    [RequireComponent(typeof(Collider))]
    public class EnemyTargeting : MonoBehaviour
    {
        [SerializeField] private List<WeightPerLayer> weightPerLayers;
        [SerializeField] private bool debugMode;

        private float _viewDistance;
        private PriorityQueue<ITarget> _priorityQueue;

        private void Awake()
        {
            _viewDistance = GetComponent<Collider>().bounds.size.magnitude / 2;
            _priorityQueue = new PriorityQueue<ITarget>((targetA, targetB) =>
            {
                float utilityA = GetUtilityScore(targetA);
                float utilityB = GetUtilityScore(targetB);

                return utilityB.CompareTo(utilityA);
            });
        }

        public ITarget GetTarget()
        {
            while (_priorityQueue.Count > 0)
            {
                var target = _priorityQueue.Peek();
                if (IsValidTarget(target))
                {
                    return target;
                }

                // Remove invalid targets
                _priorityQueue.Dequeue();
            }

            return null;
        }

        private bool IsValidTarget(ITarget target)
        {
            return target != null && target.GameObject.activeInHierarchy;
        }

        private float GetUtilityScore(ITarget target)
        {
            if (!IsValidTarget(target)) return -1;

            int targetLayer = target.GameObject.layer;
            var pair = weightPerLayers.FirstOrDefault(x => (x.layerMask.value & (1 << targetLayer)) != 0);
            if (pair.layerMask == 0) return -1;

            float weight = pair.weight;
            float distance = Vector3.Distance(target.Position, transform.position);
            // Clamped by distance
            var score = (1 - distance / _viewDistance) * weight;

            if (debugMode)
            {
                Color color = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(score));
                Debug.DrawLine(transform.position, target.Position, color, Time.deltaTime);
            }

            return score;
        }

        private void OnTriggerEnter(Collider other)
        {
            var pair = weightPerLayers.FirstOrDefault(x => (x.layerMask.value & (1 << other.gameObject.layer)) != 0);
            if (pair.layerMask == 0) return;

            if (other.TryGetComponent(out ITarget target) && IsValidTarget(target))
            {
                _priorityQueue.Enqueue(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var pair = weightPerLayers.FirstOrDefault(x => (x.layerMask.value & (1 << other.gameObject.layer)) != 0);
            if (pair.layerMask == 0) return;

            if (other.TryGetComponent(out ITarget target))
            {
                _priorityQueue.Remove(target);
            }
        }
    }

    [Serializable]
    public struct WeightPerLayer
    {
        public LayerMask layerMask;
        public float weight;
    }
}