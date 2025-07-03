using TMPro;
using UnityEngine;

namespace ZUN
{
    public class FirebaseSignIn : MonoBehaviour
    {
        Manager_FirebaseAuth manager_FirebaseAuth;
        Manager_FirebaseFirestore manager_FirebaseFirestore;
        UserDataManager userDataManager;

        [SerializeField] GameStart gameStart;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        void Awake()
        {
            manager_FirebaseAuth = Manager_FirebaseAuth.instance;
            manager_FirebaseFirestore = Manager_FirebaseFirestore.instance;
            userDataManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UserDataManager>();
        }

        public async void OnClickSignIn()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = await manager_FirebaseAuth.SignInAsync(email, password);

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