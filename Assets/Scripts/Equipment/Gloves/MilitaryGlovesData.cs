using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryGlovesData", menuName = "Scriptable Objects/Equipmet Data/Gloves/MilitaryGlovesData")]
    public class MilitaryGlovesData : GlovesData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(EquipmentTier tier)
        {
            return new MilitaryGloves(this, tier);
        }
    }
}