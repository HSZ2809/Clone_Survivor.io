using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Tutorial : MonoBehaviour
    {
        UserDataManager dataManager;
        GameDataProvider dataProvider;
        Manager_Storage storage;

        [SerializeField] int energy;
        [SerializeField] int gem;
        [SerializeField] int gold;
        [SerializeField] EquipmentInfo[] initialEquip;
        [SerializeField] ItemInfo[] initialItem;

        private void Awake() 
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<UserDataManager>(out dataManager))
            {
#if UNITY_EDITOR
                Debug.LogWarning("UserDataManager not found");
#endif
                Application.Quit();
            }
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<GameDataProvider>(out dataProvider))
            {
#if UNITY_EDITOR
                Debug.LogWarning("GameDataProvider not found");
#endif
                Application.Quit();
            }
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
            {
#if UNITY_EDITOR
                Debug.LogWarning("Manager_Storage not found");
#endif
                Application.Quit();
            }
        }

        private async void Start()
        {
            try
            {
                string uid = Manager_FirebaseAuth.instance.Auth.CurrentUser.UserId;

                if (UserDataManager.instance != null)
                {
                    if (!UserDataManager.instance.CacheData.IsTutorialCompleted)
                    {
                        // 장비 제공
                        foreach (EquipmentInfo equipInfo in initialEquip)
                        {
                            if (await dataManager.AddEquipmentAsync(uid, equipInfo))
                            {
                                Equipment equip = dataProvider.CreateEquipment(equipInfo);
                                storage.equipments.Add(equip);
                            }
                        }

                        // 아이템 제공
                        foreach (ItemInfo itemInfo in initialItem)
                        {
                            if (await dataManager.AddItemAsync(uid, itemInfo))
                            {
                                Item item = dataProvider.CreateItem(itemInfo);
                                storage.items.Add(item.Data.Id, item);
                            }
                        }

                        // 에너지, 젬, 골드 제공
                        var data = new Dictionary<string, object>
                        {
                            { "Energy", energy },
                            { "Gem",  gem },
                            { "Gold", gold }
                        };
                        if (await dataManager.UpdateUserCoreData(uid, data))
                        {
                            storage.Energy = energy;
                            storage.Gem = gem;
                            storage.Gold = gold;
                        }

                        // CacheData.IsTutorialCompleted 를 true로 변경
                        var dic = new Dictionary<string, object>
                        {
                            ["IsTutorialCompleted"] = true
                        };

                        await dataManager.UpdateUserCoreData(uid, dic);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[initialize] 실패: {ex}");
            }
        }
    }
}