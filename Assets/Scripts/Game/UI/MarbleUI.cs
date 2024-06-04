using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MarbleUI : MonoBehaviour
    {
        public Image marbleImage;
        public float targetY;

        public void Initialize(Sprite sprite)
        {
            marbleImage.sprite = sprite;
        }
    }
}