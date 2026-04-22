using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ZUN
{
    public class Btn_ChapterStart : MonoBehaviour
    {
        [SerializeField] string sceneName;
        [SerializeField] float sceneChangeTime = 2.0f;
        [Inject] IManager_Scene manager_Scene;
        [Inject] IManager_Alert manager_Alert;
        Scene currentScene;

        private void Awake()
        {
            currentScene = gameObject.scene;
        }

        public void StageStart()
        {
            StartCoroutine(SceneChange());
        }

        IEnumerator SceneChange()
        {
            yield return new WaitForSeconds(sceneChangeTime);

            if (currentScene == null)
                manager_Alert.ShowPopup("Scene change fail");
            else
                manager_Scene.LoadScene(sceneName, currentScene.buildIndex);
        }
    }
}