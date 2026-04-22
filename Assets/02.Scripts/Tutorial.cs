using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZUN
{
    public class Tutorial : MonoBehaviour
    {
        [Inject] private IManager_FirebaseAuth firebaseAuth;
        [Inject] private IUserDataManager dataManager;
        [Inject] private IGameEntityFactory dataProvider;
        [Inject] private IManager_Storage storage;

        [SerializeField] int energy;
        [SerializeField] int gem;
        [SerializeField] int gold;
        [SerializeField] EquipmentInfo[] initialEquip;
        [SerializeField] ItemInfo[] initialItem;

        private async void Start()
        {
            try
            {
                string uid = firebaseAuth.UserId;

                if (dataManager != null)
                {
                    if (!dataManager.CacheData.IsTutorialCompleted)
                    {
                        // 장비 제공
                        foreach (EquipmentInfo equipInfo in initialEquip)
                        {
                            if (await dataManager.AddEquipmentAsync(uid, equipInfo))
                            {
                                Equipment equip = dataProvider.CreateEquipment(equipInfo);
                                storage.Equipments.Add(equip);
                            }
                        }

                        // 아이템 제공
                        foreach (ItemInfo itemInfo in initialItem)
                        {
                            if (await dataManager.AddItemAsync(uid, itemInfo))
                            {
                                Item item = dataProvider.CreateItem(itemInfo);
                                storage.Items.Add(item.Data.Id, item);
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