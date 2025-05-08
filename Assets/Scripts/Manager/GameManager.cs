using UnityEngine;

namespace ZUN
{
    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        public struct Manager
        {
            public Manager_Alert alert;
            public Manager_Audio audio;
            public Manager_Inventory inventory;
            public Manager_Scene scene;
            public Manager_Storage storage;
            public Manager_Vibration vibration;
        }
        public Manager manager;
    }
}