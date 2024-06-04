using UnityEngine;

namespace Queue.Helpers
{
    public class EventSystemHelper : MonoBehaviour
    {
        private static EventSystemHelper _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);
        }
    }
}
