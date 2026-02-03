using Firebase;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class Manager_FirebaseCore : MonoBehaviour
    {
        public static Task InitializationTask { get; private set; }

        async void Awake()
        {
            InitializationTask = FirebaseApp.CheckAndFixDependenciesAsync();
            await InitializationTask;
        }
    }
}