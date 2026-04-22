using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZUN
{
    public interface IUserDataManager
    {
        UserData CacheData { get; }
        event Action<UserData> OnCoreDataChanged;
        event Action<UserData> OnEquipDataChanged;
        event Action<UserData> OnItemDataChanged;

        Task<bool> LoadAsync(string uid);
        Task<bool> UpdateUserCoreData(string uid, Dictionary<string, object> datas);
        Task<List<EquipmentInfo>> LoadEquipmentsAsync(string uid);
        Task<bool> AddEquipmentAsync(string uid, EquipmentInfo equipment);
        Task<bool> UpdateEquipmentAsync(string uid, Equipment equipment);
        Task<bool> RemoveEquipmentAsync(string uid, string equipmentUuid);
        Task<bool> EquipItemToSlot(string uid, EquipmentType slotType, string equipmentUuid);
        Task<Dictionary<string, int>> LoadItemsAsync(string uid);
        Task<bool> AddItemAsync(string uid, ItemInfo item);
        Task<bool> UpdateItemAsync(string uid, string itemUid, int newAmount);
        Task<bool> RemoveItemAsync(string uid, string itemUid);
    }
}