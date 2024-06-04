using Combat;
using Game.Combat;
using UnityEngine;
using Utility;

namespace AI
{
    public class EnemyFactory : IGameObjectFactory<IEnemyModel, Enemy>
    {
        public Enemy Create(IEnemyModel model, Vector3 position)
        {
            //instantiate a new prefab
            GameObject prefab = Object.Instantiate(model.Prefab, position, Quaternion.identity);

            //Damage
            if (prefab.TryGetComponent(out EnemyMeleeAttack attack))
            {
                attack.Init(model.Damage,
                    model.AttackWindup,
                    model.AttackCooldown,
                    model.AttackRange);
            }
            else
            {
                prefab.AddComponent<EnemyMeleeAttack>().Init(model.Damage,
                    model.AttackWindup,
                    model.AttackCooldown,
                    model.AttackRange);
            }

            //Health

            if (prefab.TryGetComponent(out IDamageable health))
            {
                health.Init(model.Health);
            }
            else
            {
                prefab.AddComponent<Health>().Init(model.Health);
            }

            //Speed
            if (prefab.TryGetComponent(out EnemyMovement movement))
            {
                movement.Init(model.Speed);
            }
            else
            {
                prefab.AddComponent<EnemyMovement>().Init(model.Speed);
            }

            if (prefab.TryGetComponent(out Enemy enemy))
            {
                enemy.Init(health, movement);
                return enemy;
            }

            enemy = prefab.AddComponent<Enemy>().Init(health, movement);
            return enemy;
        }
    }
}