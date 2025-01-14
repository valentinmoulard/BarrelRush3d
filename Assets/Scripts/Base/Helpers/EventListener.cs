using UnityEngine;

namespace Base.Helpers
{
    public abstract class EventListener: MonoBehaviour
    {
        protected abstract void SubscribeEvents();
        protected abstract void UnsubscribeEvents();
        protected void OnEnable()
        {
            SubscribeEvents();
        }
        protected void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}