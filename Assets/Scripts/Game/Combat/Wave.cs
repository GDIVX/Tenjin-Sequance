using AI;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Game/Wave", order = 0)]
    public class Wave : ScriptableObject
    {
        public EnemyModel model;
        [Range(0, 1)] public float spawnRate;
        [Range(0, 0.5f)] public float spawnRateVelocity;
        [Min(0)] public float delay;
        public float waveDuration;
        public int targetWaveSize;
    }
}