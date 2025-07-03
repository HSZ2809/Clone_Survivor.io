using TMPro;
using UnityEngine;

namespace ZUN
{
    public class FirebaseSignUp : MonoBehaviour
    {
        Manager_FirebaseAuth manager_FirebaseAuth;
        Manager_FirebaseFirestore manager_FirebaseFirestore;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        void Awake()
        {
            manager_FirebaseAuth = Manager_FirebaseAuth.instance;
            manager_FirebaseFirestore = Manager_FirebaseFirestore.instance;
        }

        public async void OnClickSignUp()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = await manager_FirebaseAuth.SignUpAsync(email, password);

            if (isSuccess)
            {
                feedbacktxt.text = $"회원가입 성공";
                string uid = manager_FirebaseAuth.Auth.CurrentUser.UserId;
                await manager_FirebaseFirestore.CreateUserDocumentAsync(uid, email);
            }
            else
                feedbacktxt.text = $"회원가입 실패";
        }
    }
}