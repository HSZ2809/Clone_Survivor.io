using UnityEngine;

namespace ZUN
{
    public class Btn_Pause : MonoBehaviour
    {
        [SerializeField] private GameObject menu = null;

        public void OpenMenu()
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}