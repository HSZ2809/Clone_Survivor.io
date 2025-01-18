using UnityEngine;

namespace ZUN
{
    public class Manager_Storage : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            Debug.Log("Manager_Storage : create");
        }
    }
}