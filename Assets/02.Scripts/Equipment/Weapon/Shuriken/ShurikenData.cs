using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "ShurikenData", menuName = "Scriptable Objects/Equipmet Data/Weapon/ShurikenData")]
    public class ShurikenData : WeaponData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(string uuid, EquipmentTier tier, int level)
        {
            return new Shuriken(uuid, this, tier, level);
        }
    }
}