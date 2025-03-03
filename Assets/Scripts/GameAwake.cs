using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class GameAwake : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float sceneChangeTime = 2.0f;
        private Manager_Scene manager_Scene;
        private Scene currentScene;

        private void Awake()
        {
            manager_Scene = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Scene>();
            currentScene = gameObject.scene;
        }

        private void Start()
        {
            StartCoroutine(SceneChange());
        }

        private IEnumerator SceneChange()
        {
            yield return new WaitForSeconds(sceneChangeTime);

            manager_Scene.LoadScene(sceneName, currentScene.buildIndex);
        }
    }
}