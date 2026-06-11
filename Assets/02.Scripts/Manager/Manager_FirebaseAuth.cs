using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ZUN
{
    public class Manager_FirebaseAuth : MonoBehaviour, IManager_FirebaseAuth
    {
        [Inject] private readonly IManager_FirebaseCore _core;
        [Inject] private readonly IManager_Alert _alert;

        public FirebaseAuth _auth;

        public string UserId => _auth.CurrentUser.UserId;

        void Awake()
        {
            _core.OnFirebaseInitialized += InitializeAuth;
        }

        void InitializeAuth()
        {
            _auth = FirebaseAuth.DefaultInstance;
#if UNITY_EDITOR
            Debug.Log("FirebaseAuth initialized.");
#endif
        }

        /// <summary>
        /// Attempts to create a new account with the given email and password.
        /// Returns true on success, false on failure.
        /// </summary>
        public async Task<bool> SignUpAsync(string email, string password)
        {
            try
            {
                await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
#if UNITY_EDITOR
                Debug.Log("Sign-up successful.");
#endif
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[SignUpAsync] Sign-up failed: {ex.Message}");
                _alert.ShowPopup($"Sign-up failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Attempts to sign in with the given email and password.
        /// Returns true on success, false on failure.
        /// </summary>
        public async Task<bool> SignInAsync(string email, string password)
        {
            try
            {
                await _auth.SignInWithEmailAndPasswordAsync(email, password);
#if UNITY_EDITOR
                Debug.Log("Sign-in successful.");
#endif
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[SignInAsync] Sign-in failed: {ex.Message}");
                _alert.ShowPopup($"Sign-in failed: {ex.Message}");
                return false;
            }
        }
    }
}
