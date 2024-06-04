using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Queue.Helpers
{
    public class CoroutineHelper : MonoSingleton<CoroutineHelper>
    {
        [SerializeField] private float _cleanUpRate;
        private float _timer;
        
        private Dictionary<string, Coroutine> _coroutine;

        public override void Awake()
        {
            base.Awake();
            _coroutine = new Dictionary<string, Coroutine>();
            _timer = _cleanUpRate;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                CleanUp();
#if UNITY_EDITOR
                Debug.Log("<color=#ff2d00>Clean up coroutine!</color>");
#endif
                _timer = _cleanUpRate;
            }
        }

        public Coroutine StartCoroutineHelper(IEnumerator coroutine,string  coroutineName = null,bool overWriteCoroutine = false)
        {
            Coroutine newCoroutine = null;

            if (!string.IsNullOrEmpty(coroutineName))
            {
                if (_coroutine.TryGetValue(coroutineName, out var routine))
                {
                    if (overWriteCoroutine)
                    {
                        StopCoroutine(routine);
                        
                        _coroutine.Remove(coroutineName);
                        
                        newCoroutine = StartCoroutine(coroutine);
                        
                        _coroutine.Add(coroutineName, newCoroutine);
                        
                        return  newCoroutine;
                    }
                    
                    Debug.LogError("Trying to start a New coroutine whit the same name (can use the overWriteCoroutine parameter to overWrite the old coroutine)");
                    return null;
                }

                newCoroutine = StartCoroutine(coroutine);
                
                _coroutine.Add(coroutineName,newCoroutine);

                return newCoroutine;
            }
            
            newCoroutine = StartCoroutine(coroutine);
            
            _coroutine.Add(nameof(coroutine), newCoroutine);
            
            return newCoroutine;
        }

        public void StopCoroutineByName(string coroutineName)
        {
            if (_coroutine.TryGetValue(coroutineName,out var coroutine))
            {
                StopCoroutine(coroutine);
                _coroutine.Remove(coroutineName);
            }

            Debug.LogError($"Can not find coroutine coroutineName: {coroutineName}");
        }

        private void CleanUp()
        {
            foreach (var keyValuePair in _coroutine)
            {
                if (keyValuePair.Value == null)
                    _coroutine.Remove(keyValuePair.Key);
            }
        }
    }
}