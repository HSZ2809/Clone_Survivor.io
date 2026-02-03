using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class GameStart : MonoBehaviour
    {
        GameDataProvider gameData;
        UserDataManager userData;
        Manager_Status status;
        Manager_Storage storage;

        [Space]
        [SerializeField] Image img_progress;
        [SerializeField] TMP_Text tmp_version;

        [Space]
        [SerializeField] string sceneName;
        [SerializeField] float sceneChangeTime;

        public bool LoginComplete = false;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<GameDataProvider>(out gameData))
                Debug.LogWarning("GameDataProvider not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<UserDataManager>(out userData))
                Debug.LogWarning("UserDataManager not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out status))
                Debug.LogWarning("Inventory not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }

        private void Start()
        {
            tmp_version.text = Application.version;
        }

        public void InitGameData()
        {
            UserData data = userData.CacheData;

            storage.Energy = data.Energy;
            storage.Gem = data.Gem;
            storage.Gold = data.Gold;

            foreach (EquipmentInfo info in data.Equipments)
            {
                storage.equipments.Add(gameData.CreateEquipment(info));
            }

            for (int i = 0; i < data.Inventory.Length; i++)
            {
                if (data.Inventory[i] != null)
                {
                    status.Inventory[i] = storage.equipments.Find(e => e.Uuid == (data.Inventory[i]));
                    storage.equipments.Remove(status.Inventory[i]);
                }
            }

            foreach (var pair in data.Items)
            {
                ItemInfo info = new (pair.Key, pair.Value);
                Item item = gameData.CreateItem(info);
                storage.items.Add(item.Data.Id ,item);
            }

            StartCoroutine(SceneLoading());
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

            img_progress.fillAmount = Mathf.Lerp(img_progress.fillAmount, 1.0f, async.progress);
            async.allowSceneActivation = true;

            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}