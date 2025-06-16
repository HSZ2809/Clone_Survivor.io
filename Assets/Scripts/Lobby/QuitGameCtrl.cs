using UnityEngine;

namespace ZUN
{
    public class QuitGameCtrl : MonoBehaviour
    {
        readonly float exitDelay = 2.0f;
        float lastBackPressedTime;

        Manager_Alert manager_Alert;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Alert>(out manager_Alert))
                Debug.LogWarning("Manager_Alert Missing!");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
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
}