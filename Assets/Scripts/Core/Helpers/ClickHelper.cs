using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Queue.Helpers
{
    public class ClickHelper : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
    {
        public event Action OnClick;
        public event Action OnEnterHover;
        public event Action OnExitHover;

        public bool IsHover { get; private set; }   
        
        private const int CLICKABLE_LAYER_INDEX = 11;
        
        private void Awake()
        {
            gameObject.layer = CLICKABLE_LAYER_INDEX;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnClick?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHover = true;
            OnEnterHover?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHover = false;
            OnExitHover?.Invoke();
        }
    }
   
}