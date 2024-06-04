using System;
using Combat;
using Game.Utility;
using Unity.Mathematics;
using UnityEngine;
using Utility;

namespace Game.Combat
{
    public sealed class ProjectileFactory : MonoBehaviour, IGameObjectFactory<ProjectileModel, Projectile>,
        IGameObjectRecycler<ProjectileModel, Projectile>
    {
        public Projectile Create(ProjectileModel model, Vector3 position)
        {
            //create the view game object as the basis for the projectile
            GameObject _gameObject = Instantiate(model.Prefab, position, quaternion.identity);

            foreach (var component in _gameObject.GetComponents<IInit<ProjectileModel>>())
            {
                component.Init(model);
            }

            if (_gameObject.TryGetComponent(out Projectile projectile))
            {
                return projectile;
            }

            return _gameObject.AddComponent<Projectile>();
        }

        public Projectile Create(ProjectileModel model, Vector3 position,
            Func<Projectile, Projectile> extendedLogic)
        {
            Projectile projectile = Create(model, position);
            return extendedLogic?.Invoke(projectile);
        }


        public Projectile Recycle(Projectile inInstance, ProjectileModel model, Vector3 position)
        {
            GameObject inGameObject = inInstance.gameObject;

            foreach (var component in inGameObject.GetComponents<IInit<ProjectileModel>>())
            {
                component.Init(model);
            }

            inGameObject.transform.position = position;

            return inInstance;
        }
    }
}