using UnityEngine;

namespace ZUN
{
    public class DummyManager_Scene : IManager_Scene
    {
        public void LoadScene(string destination, int origin)
        {
            Debug.Log($"[Dummy] LoadScene → {destination}");
        }
    }
}
