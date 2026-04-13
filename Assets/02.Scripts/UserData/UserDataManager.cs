using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class UserDataManager : MonoBehaviour
    {
        public static UserDataManager instance;

        [SerializeField] Manager_FirebaseAuth auth;
        [SerializeField] Manager_FirebaseFirestore firestore;

        public UserData CacheData { get; private set; }
        public event Action<UserData> OnCoreDataChanged;
        public event Action<UserData> OnEquipDataChanged;
        public event Action<UserData> OnItemDataChanged;

        void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;
        }

        public async Task<bool> LoadAsync(string uid)
        {
            CacheData = await firestore.GetUserDataAsync(uid);
            CacheData.Equipments = await LoadEquipmentsAsync(uid);
            CacheData.Inventory = await LoadInventoryAsync(uid);
            CacheData.Items = await LoadItemsAsync(uid);
            OnCoreDataChanged?.Invoke(CacheData);
            return CacheData != null;
        }

        public async Task<bool> UpdateUserCoreData(string uid, Dictionary<string, object> datas)
        {
            try
            {
                var docRef = firestore.Firestore.Collection("users").Document(uid);

                await docRef.UpdateAsync(datas);

                if (CacheData != null)
                {
                    foreach (var kvp in datas)
                    {
                        var property = typeof(UserData).GetProperty(kvp.Key);
                        if (property != null)
                        {
                            property.SetValue(CacheData, kvp.Value);
                        }
                    }

                    OnCoreDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateUserCoreData] 유저 데이터 업데이트 실패: {ex.Message}");
                return false;
            }
        }

        // 장비 목록 로드
        public async Task<List<EquipmentInfo>> LoadEquipmentsAsync(string uid)
        {
            var colRef = firestore.Firestore
                .Collection("users")
                .Document(uid)
                .Collection("equipments");

            var snapshot = await colRef.GetSnapshotAsync();
            var list = new List<EquipmentInfo>();

            foreach (var doc in snapshot.Documents)
            {
                var info = doc.ConvertTo<EquipmentInfo>();
                list.Add(info);
            }

            if (CacheData != null)
            {
                CacheData.Equipments = list;
                OnEquipDataChanged?.Invoke(CacheData);
            }

            return list;
        }

        // 장비 추가
        public async Task<bool> AddEquipmentAsync(string uid, EquipmentInfo equipment)
        {
            try
            {
                var colRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("equipments");

                await colRef
                    .Document(equipment.Uuid)
                    .SetAsync(equipment);

                if (CacheData?.Equipments != null)
                {
                    CacheData.Equipments.Add(equipment);
                    OnEquipDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddEquipmentAsync] 실패: {ex}");
                return false;
            }
        }

        // 장비 수정
        public async Task<bool> UpdateEquipmentAsync(string uid, Equipment equipment)
        {
            EquipmentInfo info = new EquipmentInfo(equipment);

            try
            {
                var docRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("equipments")
                    .Document(equipment.Uuid);

                var data = new Dictionary<string, object>
                {
                    { "Id",    info.Id },
                    { "Tier",  info.Tier },
                    { "Level", info.Level }
                };

                await docRef.UpdateAsync(data);

                var idx = CacheData?.Equipments.FindIndex(e => e.Uuid == info.Uuid) ?? -1;
                if (idx >= 0)
                {
                    CacheData.Equipments[idx] = info;
                    OnEquipDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateEquipmentAsync] 실패: {ex}");
                return false;
            }
        }

        // 장비 삭제
        public async Task<bool> RemoveEquipmentAsync(string uid, string equipmentUuid)
        {
            try
            {
                var docRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("equipments")
                    .Document(equipmentUuid);

                await docRef.DeleteAsync();

                CacheData?.Equipments.RemoveAll(e => e.Uuid == equipmentUuid);
                OnEquipDataChanged?.Invoke(CacheData);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[RemoveEquipmentAsync] 실패: {ex}");
                return false;
            }
        }

        public async Task<bool> EquipItemToSlot(string uid, EquipmentType slotType, string equipmentUuid)
        {
            var slotDoc = firestore.Firestore
                .Collection("users")
                .Document(uid)
                .Collection("inventory")
                .Document(slotType.ToString());

            await slotDoc.SetAsync(new Dictionary<string, object>
            {
                { "Uuid", equipmentUuid }
            }, SetOptions.MergeAll);

            if (CacheData != null)
                CacheData.Inventory[(int)slotType] = equipmentUuid;

            OnEquipDataChanged?.Invoke(CacheData);
            return true;
        }

        public async Task<string[]> LoadInventoryAsync(string uid)
        {
            var colRef = firestore.Firestore
                .Collection("users")
                .Document(uid)
                .Collection("inventory");

            var snapshot = await colRef.GetSnapshotAsync();
            var inventory = new string[Enum.GetValues(typeof(EquipmentType)).Length];

            foreach (var doc in snapshot.Documents)
            {
                if (Enum.TryParse<EquipmentType>(doc.Id, out var type))
                {
                    if (doc.TryGetValue("Uuid", out string uuid))
                    {
                        inventory[(int)type] = uuid;
                    }
                }
                else
                {
                    Debug.LogWarning($"LoadInventoryAsync: 알 수 없는 슬롯 키 '{doc.Id}'");
                }
            }

            return inventory;
        }

        public async Task<Dictionary<string, int>> LoadItemsAsync(string uid)
        {
            var colRef = firestore.Firestore
                .Collection("users")
                .Document(uid)
                .Collection("items");

            var snapshot = await colRef.GetSnapshotAsync();

            var dict = new Dictionary<string, int>();
            foreach (var doc in snapshot.Documents)
            {
                var amount = doc.GetValue<int>("Amount");
                dict[doc.Id] = amount;
            }

            CacheData.Items = dict;
            OnItemDataChanged?.Invoke(CacheData);

            return dict;
        }

        public async Task<bool> AddItemAsync(string uid, ItemInfo item)
        {
            try
            {
                var colRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("items");

                await colRef
                    .Document(item.Uid)
                    .SetAsync(new Dictionary<string, object>
                    {
                        { "Amount", item.Amount }
                    });

                if (CacheData?.Items != null)
                {
                    CacheData.Items[item.Uid] = item.Amount;
                    OnItemDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddItemAsync] 실패: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(string uid, string itemUid, int newAmount)
        {
            try
            {
                var docRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("items")
                    .Document(itemUid);

                await docRef.UpdateAsync(new Dictionary<string, object>
                {
                    { "Amount", newAmount }
                });

                if (CacheData?.Items != null)
                {
                    CacheData.Items[itemUid] = newAmount;
                    OnItemDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateItemAsync] 실패: {ex}");
                return false;
            }
        }

        public async Task<bool> RemoveItemAsync(string uid, string itemUid)
        {
            try
            {
                var docRef = firestore.Firestore
                    .Collection("users")
                    .Document(uid)
                    .Collection("items")
                    .Document(itemUid);

                await docRef.DeleteAsync();

                if (CacheData?.Items != null && CacheData.Items.Remove(itemUid))
                {
                    OnItemDataChanged?.Invoke(CacheData);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[RemoveItemAsync] 실패: {ex}");
                return false;
            }
        }
    }
}