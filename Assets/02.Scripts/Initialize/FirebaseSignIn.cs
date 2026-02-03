using TMPro;
using UnityEngine;

namespace ZUN
{
    public class FirebaseSignIn : MonoBehaviour
    {
        Manager_FirebaseAuth manager_FirebaseAuth;
        UserDataManager userDataManager;

        [SerializeField] GameStart gameStart;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_FirebaseAuth>(out manager_FirebaseAuth))
            {
                feedbacktxt.text = "FirebaseAuth not found";
                Debug.LogWarning("FirebaseAuth not found");
            }

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<UserDataManager>(out userDataManager))
            {
                feedbacktxt.text = "UserDataManager not found";
                Debug.LogWarning("UserDataManager not found");
            }
        }

        public async void OnClickSignIn()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = false;
            isSuccess = await manager_FirebaseAuth.SignInAsync(email, password);

            if (isSuccess)
            {
                feedbacktxt.text = "로그인 성공";
                string uid = manager_FirebaseAuth.Auth.CurrentUser.UserId;
                await userDataManager.LoadAsync(uid);
                gameStart.InitGameData();
            }
            else
                feedbacktxt.text = "로그인 실패";
        }
    }
}