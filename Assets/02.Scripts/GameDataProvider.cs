using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class GameDataProvider : MonoBehaviour
    {
        [SerializeField] EquipmentData[] equipmentData;
        [SerializeField] ItemData[] itemDatas;

        readonly Dictionary<string, EquipmentData> equipmentDataDic = new();
        readonly Dictionary<string, ItemData> itemDataDic = new();

        private void Start()
        {
            foreach (var equipment in equipmentData)
                equipmentDataDic.Add(equipment.Id, equipment);

            foreach (var item in itemDatas)
                itemDataDic.Add(item.Id, item);
        }

        public Equipment CreateEquipment(string id, EquipmentTier tier, int level)
        {
            return equipmentDataDic[id].Create(tier, level);
        }
    }
}