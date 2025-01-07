using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class GameAwake : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float sceneChangeTime = 2.0f;
        [SerializeField] private Manager_Scene manager_Scene;

        private void Awake()
        {
            manager_Scene = GameObject.FindGameObjectWithTag("Manager_Scene").GetComponent<Manager_Scene>();
        }

        private void Start()
        {
            StartCoroutine(SceneChange());
        }

        private IEnumerator SceneChange()
        {
            yield return new WaitForSeconds(sceneChangeTime);

            manager_Scene.LoadScene(sceneName);
        }
    }
}