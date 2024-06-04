using Combat;
using UnityEngine;

namespace AI
{
    public class EnemySpawner
    {
        public int Count => pool.CountOffPool;
        private GameObjectPool<EnemyModel, Enemy> pool;

        public EnemySpawner()
        {
            EnemyFactory factory = new();
            EnemyRecycler recycler = new();

            pool = new(factory, recycler);
        }

        public Enemy Spawn(EnemyModel model, Vector3 position)
        {
            return pool.Create(model, position);
        }

        public void Return(Enemy enemy)
        {
            pool.Return(enemy);
        }
    }
}