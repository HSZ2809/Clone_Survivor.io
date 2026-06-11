using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ZUN
{
    public class Manager_FirebaseFirestore : MonoBehaviour, IManager_FirebaseFirestore
    {
        [Inject] private readonly IManager_FirebaseCore _core;

        public FirebaseFirestore Firestore { get; private set; }

        void Awake()
        {
            _core.OnFirebaseInitialized += InitializeFirestore;
        }

        void InitializeFirestore()
        {
            Firestore = FirebaseFirestore.DefaultInstance;
#if UNITY_EDITOR
            Debug.Log("Firestore initialized.");
#endif
        }

        /// <summary>
        /// Creates the users/{uid} document with default UserData values.
        /// Returns false if the write fails.
        /// </summary>
        public async Task<bool> CreateUserDocumentAsync(string uid, string email)
        {
            try
            {
                var docRef = Firestore.Collection("users").Document(uid);
                UserData userData = new(email, "Player000001", 1);
                await docRef.SetAsync(userData);
#if UNITY_EDITOR
                Debug.Log("User document created.");
#endif
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[CreateUserDocumentAsync] Failed to create user document: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Reads users/{uid} and returns it as UserData.
        /// Returns null if the document does not exist or an error occurs.
        /// </summary>
        public async Task<UserData> GetUserDataAsync(string uid)
        {
            try
            {
                var docRef = Firestore.Collection("users").Document(uid);
                var snap = await docRef.GetSnapshotAsync();

                if (!snap.Exists)
                {
                    Debug.LogWarning($"[GetUserDataAsync] User document not found: {uid}");
                    return null;
                }

                var userData = snap.ConvertTo<UserData>();
#if UNITY_EDITOR
                Debug.Log($"[GetUserDataAsync] User data loaded: {userData.Name}");
#endif
                return userData;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GetUserDataAsync] Failed to load user data: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Updates a single field on the users/{uid} document.
        /// </summary>
        public async Task<bool> UpdateFieldAsync<T>(string uid, string fieldName, T value)
        {
            try
            {
                await Firestore.Collection("users").Document(uid).UpdateAsync(fieldName, value);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateFieldAsync] Failed to update '{fieldName}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates multiple fields on the users/{uid} document in a single write.
        /// </summary>
        public async Task<bool> UpdateFieldsAsync(string uid, Dictionary<string, object> updates)
        {
            try
            {
                await Firestore.Collection("users").Document(uid).UpdateAsync(updates);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateFieldsAsync] Failed to update multiple fields: {ex.Message}");
                return false;
            }
        }
    }
}
