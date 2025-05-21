using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "ShurikenData", menuName = "Scriptable Objects/ShurikenData")]
    public class ShurikenData : WeaponData
    {
        public override Equipment Create(EquipmentTier tier)
        {
            return new Shuriken(this, tier);
        }
    }
}