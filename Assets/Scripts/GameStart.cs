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
        private Manager_Scene manager_Scene;
        private Scene currentScene;


        private void Awake()
        {
            manager_Scene = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Scene>();
            currentScene = gameObject.scene;
        }

        private void Start()
        {
            StartCoroutine(SceneLoading(sceneName));
        }

        IEnumerator SceneLoading(string sceneName)
        {
            yield return new WaitForSeconds(sceneChangeTime);

            manager_Scene.LoadScene(sceneName, currentScene.buildIndex);

            while (!manager_Scene.Async.isDone)
            {
                yield return null;

                img_progress.fillAmount = Mathf.Lerp(img_progress.fillAmount, 1.0f, manager_Scene.Async.progress);
            }
        }
    }
}