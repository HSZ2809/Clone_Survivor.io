using UnityEngine;

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
        Manager_Alert manager_Alert;

        private void Awake()
        {
            manager_Alert = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Alert>();
        }

        public void ApplyActiveStates()
        {
            if (gameObjectActivePairs.Length < 1)
                manager_Alert.GetPopup("대상을 찾지 못했습니다");
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