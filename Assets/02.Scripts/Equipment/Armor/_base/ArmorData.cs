using UnityEngine;

namespace ZUN
{
    public abstract class ArmorData : EquipmentData
    {
        private void OnEnable()
        {
            Type = EquipmentType.Armor;
        }
    }
}