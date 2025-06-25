using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_Block : MonoBehaviour
    {
        private void Awake()
        {
            if (!TryGetComponent<Button>(out var button))
                Debug.LogWarning("Button Component Missing!");
            else
            {
                if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Alert>(out var manager_Alert))
                    Debug.LogWarning("Manager_Alert Missing!");
                else
                {
                    button.onClick.AddListener(() =>
                    {
                        manager_Alert.ShowPopup("현재 사용할 수 없는 버튼입니다");
                    });
                }
            }
        }
    }
}