using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZUN
{
    public class Btn_SetActive : MonoBehaviour
    {
        [System.Serializable]
        public class GameObjectActivePair
        {
            public GameObject gameObject;
            public bool isActive;

            public void ApplyActiveState()
            {
                if (gameObject != null)
                    gameObject.SetActive(isActive);
            }
        }

        [SerializeField] GameObjectActivePair[] gameObjectActivePairs;
        [Inject] private IManager_Alert manager_Alert;

        private void Awake()
        {
            if (!TryGetComponent<Button>(out var button))
                Debug.LogWarning("Button Component Missing!");
            else
            {
                button.onClick.AddListener(() =>
                {
                    ApplyActiveStates();
                });
            }
        }

        public void ApplyActiveStates()
        {
            if (gameObjectActivePairs.Length < 1)
                manager_Alert.ShowPopup("대상을 찾지 못했습니다");
            else
            {
                foreach (var pair in gameObjectActivePairs)
                {
                    pair.ApplyActiveState();
                }
            }
        }
    }
}