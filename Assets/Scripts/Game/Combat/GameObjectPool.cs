using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Combat
{
    [Serializable]
    public class GameObjectPool<TModel, TInstance>
        where TInstance : MonoBehaviour, IPoolable
    {
        private IGameObjectFactory<TModel, TInstance> factory;
        private IGameObjectRecycler<TModel, TInstance> recycler;
        [SerializeField] Stack<TInstance> pool;

        public GameObjectPool(IGameObjectFactory<TModel, TInstance> factory,
            IGameObjectRecycler<TModel, TInstance> recycler)
        {
            this.factory = factory;
            this.recycler = recycler;
            pool = new();
        }

        public int CountInPool => pool.Count;
        public int CountOffPool { get; private set; }

        public TInstance Create(TModel model, Vector3 position)
        {
            TInstance obj;
            //if we have objects in the stack, we recycle it
            obj = pool.Count > 0 ? recycler.Recycle(pool.Pop(), model, position) : factory.Create(model, position);
            obj.gameObject.SetActive(true);
            obj.IsActive = true;
            CountOffPool++;
            return obj;
        }

        public void Return(TInstance instance)
        {
            pool.Push(instance);
            instance.gameObject.SetActive(false);
            instance.IsActive = false;
            CountOffPool--;
        }

        public TInstance Borrow(TModel model, Vector3 position, Action<TInstance> returnDelegate)
        {
            returnDelegate += Return;

            return Create(model, position);
        }
    }
}