using UnityEngine;

namespace ZUN
{
    public class DummyManager_Alert : IManager_Alert
    {
        public void ShowPopup(string message)
        {
            Debug.Log($"[Dummy] Alert: {message}");
        }
    }
}
