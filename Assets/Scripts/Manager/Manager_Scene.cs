using System.Collections;
using System.Threading;
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

            while (!async.isDone)
            {
                yield return null;
            }

            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            //async.allowSceneActivation = false;

            //while (async.progress < 0.9f)
            //{
            //    yield return null;
            //}

            //async.allowSceneActivation = true;

            while (!async.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            isSceneChangeable = true;
        }
    }
}
