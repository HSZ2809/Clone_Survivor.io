using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_Block : MonoBehaviour
    {
        private void Start()
        {
            if (TryGetComponent<Button>(out var button))
            {
                if (GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Alert>(out var manager_Alert))
                {
                    button.onClick.AddListener(() =>
                    {
                        manager_Alert.GetPopup("현재 사용할 수 없는 버튼입니다");
                    });
                }
                else
                {
                    Debug.LogWarning("Manager_Alert Missing!");
                }
            }
            else
            {
                Debug.LogWarning("Button Component Missing!");
            }
        }
    }
}