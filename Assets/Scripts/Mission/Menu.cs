using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject pause = null;

        [Space]
        [SerializeField] private Image[] img_ownedActives;

        [Space]
        [SerializeField] private Image[] img_ownedPassives;


        public void Continue()
        {
            pause.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void Home()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}