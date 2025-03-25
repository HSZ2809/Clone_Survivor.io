using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] Image img_progress;
        [SerializeField] TMP_Text tmp_progress;
        [SerializeField] TMP_Text tmp_tesk;

        [SerializeField] private string sceneName;
        [SerializeField] private float sceneChangeTime;

        private void Start()
        {
            StartCoroutine(SceneLoading());
        }

        IEnumerator SceneLoading()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;

                img_progress.fillAmount = Mathf.Lerp(img_progress.fillAmount, 1.0f, async.progress);
            }

            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
