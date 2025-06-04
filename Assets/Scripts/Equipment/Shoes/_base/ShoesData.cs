using UnityEngine;

namespace ZUN
{
    public abstract class ShoesData : EquipmentData
    {
        private void OnEnable()
        {
            Type = EquipmentType.Shoes;
        }
    }
}