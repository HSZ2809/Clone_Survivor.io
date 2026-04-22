using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

namespace ZUN
{
    public class GameStart : MonoBehaviour
    {
        [Inject] private IGameEntityFactory gameData;
        [Inject] private IUserDataManager userData;
        [Inject] private IManager_Status status;
        [Inject] private IManager_Storage storage;

        [Space]
        [SerializeField] Image img_progress;
        [SerializeField] TMP_Text tmp_version;

        [Space]
        [SerializeField] string sceneName;
        [SerializeField] float sceneChangeTime;

        public bool LoginComplete = false;

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
                storage.Equipments.Add(gameData.CreateEquipment(info));
            }

            for (int i = 0; i < data.Inventory.Length; i++)
            {
                if (data.Inventory[i] != null)
                {
                    status.Inventory[i] = storage.Equipments.Find(e => e.Uuid == (data.Inventory[i]));
                    storage.Equipments.Remove(status.Inventory[i]);
                }
            }

            foreach (var pair in data.Items)
            {
                ItemInfo info = new (pair.Key, pair.Value);
                Item item = gameData.CreateItem(info);
                storage.Items.Add(item.Data.Id ,item);
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