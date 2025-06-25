using Firebase.Auth;
using TMPro;
using UnityEngine;

namespace ZUN
{
    public class FirebaseSignIn : MonoBehaviour
    {
        [SerializeField] Manager_FirebaseAuth manager_FirebaseAuth;

        [SerializeField] GameStart gameStart;

        [Space]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_Text feedbacktxt;

        void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_FirebaseAuth>(out manager_FirebaseAuth))
                Debug.LogWarning("FirebaseAuth not found");
        }

        public async void OnClickSignIn()
        {
            string email = emailInput.text;
            string password = passwordInput.text;

            bool isSuccess = await manager_FirebaseAuth.SignInAsync(email, password);

            if (isSuccess)
            {
                feedbacktxt.text = $"로그인 성공";
                gameStart.TEST();
            }
            else
                feedbacktxt.text = $"로그인 실패";
        }
    }
}