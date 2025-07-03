using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryBeltData", menuName = "Scriptable Objects/Equipmet Data/Belt/MilitaryBeltData")]
    public class MilitaryBeltData : BeltData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(EquipmentTier tier, int level)
        {
            return new MilitaryBelt(this, tier, level);
        }
    }
}