using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class Manager_Scene : MonoBehaviour
    {
        [SerializeField] IrisTransition transition;
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

            // Debug.Log("Manager_Scene >> Scene Load : " + sceneName);

            transition.IrisOut();
            yield return new WaitForSecondsRealtime(transition.GetIrisOutClipLength());
            
            async = SceneManager.UnloadSceneAsync(origin);

            while (!async.isDone)
            {
                yield return null;
            }

            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            Time.timeScale = 1.0f;
            transition.IrisIn();

            isSceneChangeable = true;
        }
    }
}
