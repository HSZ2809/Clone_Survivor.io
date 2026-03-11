using Firebase;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class Manager_FirebaseCore : MonoBehaviour
    {
        public static Manager_FirebaseCore instance;
        public Task InitializationTask { get; private set; }

        async void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;

            InitializationTask = FirebaseApp.CheckAndFixDependenciesAsync();
            await InitializationTask;
        }
    }
}