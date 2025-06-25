using UnityEngine;

namespace ZUN
{
    public abstract class GlovesData : EquipmentData
    {
        private void OnEnable()
        {
            Type = EquipmentType.Gloves;
        }
    }
}