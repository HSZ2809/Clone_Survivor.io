using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ZUN
{
    public class QuitGameCtrl : MonoBehaviour
    {
        readonly float exitDelay = 2.0f;
        float lastBackPressedTime;

        [Inject] IManager_Alert manager_Alert;
        InputAction quitAction;

        private void Awake()
        {
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