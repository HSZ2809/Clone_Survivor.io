using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZUN
{
    public class Btn_Block : MonoBehaviour
    {
        [Inject] private IManager_Alert manager_Alert;

        private void Awake()
        {
            if (!TryGetComponent<Button>(out var button))
                Debug.LogWarning("Button Component Missing!");
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