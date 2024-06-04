using UnityEngine;

namespace Utility
{
    public interface IGameObjectFactory<in TModel, out TInstance> where TInstance : MonoBehaviour,IPoolable
    {
        TInstance Create(TModel model , Vector3 position);
    }
}