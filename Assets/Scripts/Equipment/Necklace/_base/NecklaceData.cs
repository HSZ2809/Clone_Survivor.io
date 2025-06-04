using UnityEngine;

namespace ZUN
{
    public abstract class NecklaceData : EquipmentData
    {
        private void OnEnable()
        {
            Type = EquipmentType.Necklace;
        }
    }
}