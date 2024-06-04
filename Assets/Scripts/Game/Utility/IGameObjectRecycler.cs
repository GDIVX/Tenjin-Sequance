using UnityEngine;

namespace Utility
{
    public interface IGameObjectRecycler<TModel, TInstance> where TInstance : MonoBehaviour, IPoolable
    {
        TInstance Recycle(TInstance inInstance, TModel model, Vector3 position);
    }
}