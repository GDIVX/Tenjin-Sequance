using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class ObjectPool<T> where T : Component
    {
        private readonly T prefab;
        private readonly Queue<T> objects = new Queue<T>();

        public ObjectPool(T prefab)
        {
            this.prefab = prefab;
        }

        public T Get()
        {
            if (objects.Count == 0)
            {
                AddObjects(1);
            }

            return objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            objects.Enqueue(objectToReturn);
        }

        private void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T newObject = Object.Instantiate(prefab);
                newObject.gameObject.SetActive(false);
                objects.Enqueue(newObject);
            }
        }
    }
}