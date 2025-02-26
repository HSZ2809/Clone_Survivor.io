using UnityEngine.Events;

namespace ZUN
{
    [System.Serializable]
    public class TimedEvent
    {
        public float triggerTime;
        public UnityEvent onEvent;
    }
}