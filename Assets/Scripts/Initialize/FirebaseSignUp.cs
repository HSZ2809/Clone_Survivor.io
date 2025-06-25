using Firebase.Auth;
using TMPro;
using UnityEngine;

namespace ZUN
{
    public class FirebaseSignUp : MonoBehaviour
    {
        [SerializeField] Manager_FirebaseAuth manager_FirebaseAuth;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_FirebaseAuth>(out manager_FirebaseAuth))
                Debug.LogWarning("FirebaseAuth not found");
        }

        public async void OnClickSignUp()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = await manager_FirebaseAuth.SignUpAsync(email, password);

            if (isSuccess)
                feedbacktxt.text = $"회원가입 성공";
            else
                feedbacktxt.text = $"회원가입 실패";
        }
    }
}