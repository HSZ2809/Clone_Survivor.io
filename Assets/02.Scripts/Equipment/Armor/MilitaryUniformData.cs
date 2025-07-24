using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryUniformData", menuName = "Scriptable Objects/Equipmet Data/Armor/MilitaryUniformData")]
    public class MilitaryUniformData : ArmorData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(string uuid, EquipmentTier tier, int level)
        {
            return new MilitaryUniform(uuid, this, tier, level);
        }
    }
}