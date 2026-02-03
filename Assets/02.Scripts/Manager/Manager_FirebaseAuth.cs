using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class Manager_FirebaseAuth : MonoBehaviour
    {
        public static Manager_FirebaseAuth instance;
        Manager_Alert alert;

        public FirebaseAuth Auth { get; private set; }

        async void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;

            await Manager_FirebaseCore.InitializationTask;
            Auth = FirebaseAuth.DefaultInstance;

//            var depStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

//            if (depStatus == DependencyStatus.Available)
//                Auth = FirebaseAuth.DefaultInstance;
//            else
//            {
//                Application.Quit();
//#if UNITY_EDITOR
//                Debug.LogError($"Firebase dependency error: {depStatus}");
//                UnityEditor.EditorApplication.isPlaying = false;
//#endif
//            }

            alert = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Alert>();
        }

        /// <summary>
        /// 이메일/비밀번호 가입 시도.
        /// 성공하면 true, 실패(예외 포함)이면 false 반환.
        /// </summary>
        public async Task<bool> SignUpAsync(string email, string password)
        {
            try
            {
                // await 후에도 ContinueWithOnMainThread를 쓰고 싶으면
                // await Auth.CreateUserWithEmailAndPasswordAsync(email, password)
                //       .ContinueWithOnMainThread(_ => {});

                // 단순 처리라면 바로 await
                var cred = await Auth.CreateUserWithEmailAndPasswordAsync(email, password);
                Debug.Log("가입 성공");
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"가입 실패: {ex.Message}");
                alert.ShowPopup($"가입 실패: {ex.Message}");
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
                var test = await Auth.SignInWithEmailAndPasswordAsync(email, password);
                alert.ShowPopup($"로그인 : {test.User}");
                Debug.Log("로그인 성공");
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"로그인 실패: {ex.Message}");
                alert.ShowPopup($"로그인 실패: {ex.Message}");
                return false;
            }
        }
    }
}