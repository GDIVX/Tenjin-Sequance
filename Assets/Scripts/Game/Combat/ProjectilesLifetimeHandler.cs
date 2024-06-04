using System.Collections;
using Game.Combat;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(ProjectileFactory))]
    public class ProjectilesLifetimeHandler : MonoBehaviour
    {
        [SerializeField] private GameObjectPool<ProjectileModel, Projectile> pool;
        [SerializeField] private ProjectileFactory factory;

        private void Awake()
        {
            // Ensure that the factory is properly initialized
            factory ??= GetComponent<ProjectileFactory>();
            if (factory == null)
            {
                Debug.LogError("ProjectileFactory component is required but not found on the GameObject.", this);
                return;
            }

            pool = new GameObjectPool<ProjectileModel, Projectile>(factory, factory);
        }

        public Projectile Create(ProjectileModel model, Vector3 position, Quaternion rotation)
        {
            if (model == null)
            {
                Debug.LogError("Attempted to create a projectile with a null model.", this);
                return null;
            }

            var projectile = pool.Create(model, position);
            if (projectile == null)
            {
                Debug.LogError("Failed to create a projectile from the pool.", this);
                return null;
            }

            projectile.transform.localRotation = rotation;
            SetupProjectile(projectile, model);

            return projectile;
        }

        public Projectile Create(ProjectileModel model, Vector3 spawnPosition, ITarget target)
        {
            if (model == null)
            {
                Debug.LogError("Attempted to create a projectile with a null model.", this);
                return null;
            }

            var projectile = pool.Create(model, spawnPosition);
            if (projectile == null)
            {
                Debug.LogError("Failed to create a projectile from the pool.", this);
                return null;
            }

            SetupProjectile(projectile, model);

            return projectile;
        }

        private void SetupProjectile(Projectile projectile, ProjectileModel model)
        {
            // Ensure damage component is active and listeners are set up properly
            var damageComponent = projectile.GetComponent<DamageOnCollision>();
            if (damageComponent != null)
            {
                damageComponent.OnCollision.AddListener((damage, collision) =>
                {
                    if (!projectile.IsActive) return;
                    pool.Return(projectile);
                });
            }
            else
            {
                Debug.LogWarning("Projectile does not have a DamageOnCollision component.", projectile);
            }

            // Start the coroutine to handle the lifetime of the projectile
            StartCoroutine(HandleProjectileLifetime(projectile, model.Lifetime));
        }

        IEnumerator HandleProjectileLifetime(Projectile projectile, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            if (projectile != null && projectile.IsActive)
            {
                pool.Return(projectile);
            }
        }
    }
}