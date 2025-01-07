using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class Manager_Scene : MonoBehaviour
    {
        private bool isSceneChangeable = true;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void LoadScene(string sceneName)
        {
            if (isSceneChangeable)
            {
                isSceneChangeable = false;

                StartCoroutine(LoadTargetScene(sceneName));
            }
        }

        IEnumerator LoadTargetScene(string sceneName)
        {
#if UNITY_EDITOR
            Debug.Log("Manager_Scene >> Scene Load : " + sceneName);
#endif

            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            isSceneChangeable = true;

            yield return true;
        }
    }
}
