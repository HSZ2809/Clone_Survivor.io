using Firebase;
using System;
using UnityEngine;

namespace ZUN
{
    public class Manager_FirebaseCore : MonoBehaviour, IManager_FirebaseCore
    {
        public event Action OnFirebaseInitialized;

        private async void Start()
        {
            var result = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (result == DependencyStatus.Available)
            {
                OnFirebaseInitialized?.Invoke();
            }
            else
            {
                Debug.LogError("Firebase dependencies not available: " + result);
            }
        }
    }
}