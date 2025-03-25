using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

namespace ZUN
{
    public class GameAwake : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float sceneChangeTime = 2.0f;

        private void Start()
        {
            StartCoroutine(SceneChange());
        }

        private IEnumerator SceneChange()
        {
            yield return new WaitForSeconds(sceneChangeTime);

            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;
            }

            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
