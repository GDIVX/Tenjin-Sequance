using Combat;
using Game.Combat;
using UnityEngine;
using Utility;

namespace AI
{
    public class EnemyRecycler : IGameObjectRecycler<EnemyModel , Enemy>
    {

        public Enemy Recycle(Enemy inInstance, EnemyModel model, Vector3 position)
        {

            inInstance.transform.position = position;
            
            //TODO : Damage

            //Health

            if (inInstance.TryGetComponent(out IDamageable health))
            {
                health.Init(model.Health);
            }
            else
            {
                inInstance.gameObject.AddComponent<Health>().Init(model.Health);
            }

            //Speed
            if (inInstance.TryGetComponent(out EnemyMovement movement))
            {
                movement.Init(model.Speed);
            }
            else
            {
                inInstance.gameObject.AddComponent<EnemyMovement>().Init(model.Speed);
            }

            if (inInstance.TryGetComponent(out Enemy enemy))
            {
                enemy.Init(health, movement);
                return enemy;
            }

            enemy = inInstance.gameObject.AddComponent<Enemy>().Init(health, movement);
            return enemy;
        }
    }
}