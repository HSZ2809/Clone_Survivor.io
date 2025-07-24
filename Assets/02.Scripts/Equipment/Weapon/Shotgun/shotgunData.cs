using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "ShotgunData", menuName = "Scriptable Objects/Equipmet Data/Weapon/ShotgunData")]
    public class ShotgunData : WeaponData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(string uuid, EquipmentTier tier, int level)
        {
            return new Shotgun(uuid, this, tier, level);
        }
    }
}