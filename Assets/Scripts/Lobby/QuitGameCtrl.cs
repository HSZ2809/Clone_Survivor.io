using UnityEngine;
using UnityEngine.InputSystem;

namespace ZUN
{
    public class QuitGameCtrl : MonoBehaviour
    {
        readonly float exitDelay = 2.0f;
        float lastBackPressedTime;

        Manager_Alert manager_Alert;
        InputAction quitAction;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Alert>(out manager_Alert))
                Debug.LogWarning("Manager_Alert Missing!");

            quitAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
            quitAction.performed += ctx => OnQuitPressed();
            quitAction.Enable();
        }

        private void OnQuitPressed()
        {
            if (Time.time - lastBackPressedTime < exitDelay)
            {
                Application.Quit();
            }
            else
            {
                lastBackPressedTime = Time.time;
                manager_Alert.ShowPopup("한 번 더 누르면 종료합니다");
            }
        }
    }
}