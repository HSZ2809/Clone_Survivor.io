using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class Btn_ChapterStart : MonoBehaviour
    {
        [SerializeField] string sceneName;
        [SerializeField] float sceneChangeTime = 2.0f;
        Manager_Scene manager_Scene;
        Manager_Alert manager_Alert;
        Scene currentScene;

        private void Awake()
        {
            GameObject manager = GameObject.FindGameObjectWithTag("Manager");
            if (manager == null)
                Debug.LogWarning("Manager not found");
            else
            {
                manager.TryGetComponent<Manager_Scene>(out manager_Scene);
                manager.TryGetComponent<Manager_Alert>(out manager_Alert);
            }

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
                manager_Alert.GetPopup("Scene change fail");
            else
                manager_Scene.LoadScene(sceneName, currentScene.buildIndex);
        }
    }
}