using UnityEngine;

namespace ZUN
{
    public abstract class BeltData : EquipmentData
    {
        private void OnEnable()
        {
            Type = EquipmentType.Belt;
        }
    }
}