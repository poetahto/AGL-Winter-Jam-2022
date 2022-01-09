using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class ButtonScaler : MonoBehaviour
    {
        [SerializeField] private EventTrigger eventTrigger;
        
        [SerializeField] private RectTransform hoverTransform;
        [SerializeField] private float hoverTime = 0.1f;
        [SerializeField] private float hoverScale = 1.05f;

        [SerializeField] private RectTransform pressTransform;
        [SerializeField] private float pressTime = 0.1f;
        [SerializeField] private float pressScale = 0.95f;

        private void Start()
        {
            RegisterTrigger(OnPointerUp, EventTriggerType.PointerUp);
            RegisterTrigger(OnPointerDown, EventTriggerType.PointerDown);
            RegisterTrigger(OnPointerEnter, EventTriggerType.PointerEnter);
            RegisterTrigger(OnPointerExit, EventTriggerType.PointerExit);
        }

        private void RegisterTrigger(UnityAction<BaseEventData> callback, EventTriggerType type)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener(callback);
            eventTrigger.triggers.Add(entry);
        }

        public void OnPointerDown(BaseEventData arg0)
        {
            pressTransform.DOScale(pressScale, pressTime);
        }

        public void OnPointerUp(BaseEventData arg0)
        {
            pressTransform.DOScale(1, pressTime);
        }

        public void OnPointerEnter(BaseEventData arg0)
        {
            hoverTransform.DOScale(hoverScale, hoverTime);
        }

        public void OnPointerExit(BaseEventData arg0)
        {
            hoverTransform.DOScale(1, hoverTime);
        }
    }
}