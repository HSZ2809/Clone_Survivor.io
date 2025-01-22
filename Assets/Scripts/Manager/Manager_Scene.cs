using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class Manager_Scene : MonoBehaviour
    {
        private bool isSceneChangeable = true;
        AsyncOperation async;

        public AsyncOperation Async {  get { return async; } }

        private void Start()
        {
            async = SceneManager.LoadSceneAsync("Splash", LoadSceneMode.Additive);
        }

        public void LoadScene(string destination, int origin)
        {
            if (isSceneChangeable)
            {
                isSceneChangeable = false;

                StartCoroutine(LoadTargetScene(destination, origin));
            }
        }

        IEnumerator LoadTargetScene(string sceneName, int origin)
        {
            #if UNITY_EDITOR
            Debug.Log("Manager_Scene >> Scene Load : " + sceneName);
            #endif

            async = SceneManager.UnloadSceneAsync(origin);
            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            yield return new WaitUntil(() => async.isDone); 
            isSceneChangeable = true;
        }
    }
}
