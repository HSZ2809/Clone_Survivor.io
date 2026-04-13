using UnityEngine;

namespace ZUN
{
    public interface IGameDataAssets
    {
        EquipmentData[] GetEquipmentData();
        ItemData[] GetItemData();
    }

    public class GameDataAssets : MonoBehaviour, IGameDataAssets
    {
        [SerializeField] private EquipmentData[] equipmentData;
        [SerializeField] private ItemData[] itemData;

        public EquipmentData[] GetEquipmentData() => equipmentData;
        public ItemData[] GetItemData() => itemData;
    }
}
