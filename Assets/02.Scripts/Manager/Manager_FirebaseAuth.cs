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


        // async void Awake()
        // {
        //     await firebaseCoreManager.InitializationTask;
        //     _auth = FirebaseAuth.DefaultInstance;
        // }

        void Awake()
        {
            _core.OnFirebaseInitialized += InitializeAuth;
        }

        void InitializeAuth()
        {
            _auth = FirebaseAuth.DefaultInstance;
            Debug.Log("FirebaseAuth 초기화 완료");
        }

        /// <summary>
        /// 이메일/비밀번호 가입 시도.
        /// 성공하면 true, 실패(예외 포함)이면 false 반환.
        /// </summary>
        public async Task<bool> SignUpAsync(string email, string password)
        {
            try
            {
                var cred = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
                Debug.Log("가입 성공");
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"가입 실패: {ex.Message}");
                _alert.ShowPopup($"가입 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 이메일/비밀번호 로그인 시도.
        /// 성공하면 true, 실패(예외 포함)이면 false 반환.
        /// </summary>
        public async Task<bool> SignInAsync(string email, string password)
        {
            try
            {
                var test = await _auth.SignInWithEmailAndPasswordAsync(email, password);
                Debug.Log("로그인 성공");
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"로그인 실패: {ex.Message}");
                _alert.ShowPopup($"로그인 실패: {ex.Message}");
                return false;
            }
        }
    }
}