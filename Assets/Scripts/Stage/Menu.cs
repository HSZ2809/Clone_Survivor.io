using UnityEngine;

namespace ZUN
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject pause = null;

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