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
        [SerializeField] TMP_Text tmp_version;
        [SerializeField] TMP_Text tmp_progress;
        [SerializeField] TMP_Text tmp_tesk;

        [SerializeField] private string sceneName;
        [SerializeField] private float sceneChangeTime;

        public bool tempEquipmentDispenserLoding = false;
        public bool LoginComplete = false;

        public void TEST()
        {
            StartCoroutine(SceneLoading());
        }

        private void Start()
        {
            tmp_version.text = Application.version;
        }

        IEnumerator SceneLoading()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                img_progress.fillAmount = Mathf.Lerp(img_progress.fillAmount, 1.0f, async.progress);

                yield return null;
            }

            yield return new WaitUntil(() => tempEquipmentDispenserLoding);
            img_progress.fillAmount = Mathf.Lerp(img_progress.fillAmount, 1.0f, async.progress);
            async.allowSceneActivation =true;

            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
