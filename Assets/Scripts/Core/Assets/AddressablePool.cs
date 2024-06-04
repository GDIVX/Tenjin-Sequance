using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Core.Assets
{
    public class AddressablePool<T> where T : class
    {
        List<T> _values;
        string _address;

        public AddressablePool(string address)
        {
            _address = address;
            _values = new List<T>();
        }

        /// <summary>
        /// Get an asset from the pool or load it from the addressable
        /// </summary>
        /// <returns></returns>
        public Task<T> GetAsync()
        {
            //If the asset is in the pool, return it
            if (_values.Count > 0)
            {
                T value = _values[0];
                _values.RemoveAt(0);
                return Task.FromResult(value);
            }
            return Addressables.LoadAssetAsync<T>(_address).Task;
        }

        /// <summary>
        /// Return an asset to the pool
        /// </summary>
        /// <param name="value"></param>
        public void Return(T value)
        {
            _values.Add(value);
        }
    }
}