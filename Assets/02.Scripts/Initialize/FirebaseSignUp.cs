using TMPro;
using UnityEngine;
using Zenject;

namespace ZUN
{
    public class FirebaseSignUp : MonoBehaviour
    {
        [Inject] private IManager_FirebaseAuth manager_FirebaseAuth;
        [Inject] private IManager_FirebaseFirestore manager_FirebaseFirestore;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        public async void OnClickSignUp()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = await manager_FirebaseAuth.SignUpAsync(email, password);

            if (isSuccess)
            {
                feedbacktxt.text = $"회원가입 성공";
                string uid = manager_FirebaseAuth.UserId;
                await manager_FirebaseFirestore.CreateUserDocumentAsync(uid, email);
            }
            else
                feedbacktxt.text = $"회원가입 실패";
        }
    }
}