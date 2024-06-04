using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace AI
{
    public interface IReturner
    {
        void ReturnToPool(Enemy enemy);
    }

    public class EnemySpawnManager : MonoBehaviour, IReturner
    {
        [SerializeField] private List<Wave> waves;
        [SerializeField] private float spawnRadius;

        [SerializeField] private int currentWaveIndex = -1;

        private Wave CurrentWave
        {
            get
            {
                if (currentWaveIndex >= waves.Count) return null;
                return waves[currentWaveIndex];
            }
        }

        private float waveTime;

        private EnemySpawner spawner;

        public void ReturnToPool(Enemy enemy)
        {
            spawner.Return(enemy);
        }

        private void Start()
        {
            spawner = new();

            StartNextWave();
        }

        private void Update()
        {
            if (currentWaveIndex >= waves.Count) return;
            if (waveTime <= 0)
            {
                StartNextWave();
                return;
            }


            waveTime -= Time.fixedDeltaTime;
        }

        private IEnumerator SpawnWave()
        {
            yield return new WaitForSeconds(CurrentWave.delay);
            if (currentWaveIndex >= waves.Count) yield break;
            if (CurrentWave == null) yield break;

            //calculate how many units to spawn
            var spawnSize = GetSpawnSize();

            Spawn(CurrentWave.model, spawnSize);

            yield return SpawnWave();
        }

        private int GetSpawnSize()
        {
            if (currentWaveIndex >= waves.Count) return 0;

            var currSize = spawner.Count;
            var targetSize = CurrentWave.targetWaveSize;
            var spawnSizeEst = Mathf.Lerp(currSize, targetSize, CurrentWave.spawnRate);
            spawnSizeEst = Mathf.Clamp(spawnSizeEst, 0, targetSize - currSize);
            int spawnSize = (int)Mathf.Floor(spawnSizeEst);
            return spawnSize;
        }

        private void StartNextWave()
        {
            waveTime = CurrentWave.waveDuration;
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Count) return;

            StartCoroutine(SpawnWave());
        }

        private void Spawn(EnemyModel model, int size)
        {
            for (int i = 0; i < size; i++)
            {
                var circle = GetRandomPointOnCircleEdge() * spawnRadius;
                Vector3 position = new Vector3(circle.x, 0f, circle.y) + transform.position;

                spawner.Spawn(model, position);
            }
        }

        public static Vector2 GetRandomPointOnCircleEdge()
        {
            float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
            Vector2 pointOnCircle = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            return pointOnCircle;
        }

        // IEnumerator SpawnWaveCor(Wave wave)
        // {
        //     if (wave.waveDuration <= 0)
        //     {
        //         StartNextWave();
        //         yield break;
        //     }
        //
        //
        //     wave.waveDuration -= wave.delay;
        //
        //
        //     //Debug.Log($"Spawning {spawnSize} enemies out of {targetSize} -> current size is {spawner.Count}");
        //
        //     yield return Spawn(wave.model, spawnSize, wave.delay);
        //
        //     wave.waveDuration -= wave.delay;
        //
        //     wave.spawnRate = Mathf.Clamp(wave.spawnRate, wave.spawnRate + wave.spawnRateVelocity, 1);
        //     yield return SpawnWaveCor(wave);
        // }
    }
}