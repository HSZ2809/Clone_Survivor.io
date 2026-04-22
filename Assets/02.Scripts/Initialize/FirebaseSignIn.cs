using TMPro;
using UnityEngine;
using Zenject;

namespace ZUN
{
    public class FirebaseSignIn : MonoBehaviour
    {
        [Inject] private IManager_FirebaseAuth manager_FirebaseAuth;
        [Inject] private IUserDataManager userDataManager;

        [SerializeField] GameStart gameStart;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        public async void OnClickSignIn()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = false;
            isSuccess = await manager_FirebaseAuth.SignInAsync(email, password);

            if (isSuccess)
            {
                feedbacktxt.text = "로그인 성공";
                string uid = manager_FirebaseAuth.UserId;
                await userDataManager.LoadAsync(uid);
                gameStart.InitGameData();
            }
            else
                feedbacktxt.text = "로그인 실패";
        }
    }
}