using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryNecklaceData", menuName = "Scriptable Objects/Equipmet Data/Necklace/MilitaryNecklaceData")]
    public class MilitaryNecklaceData : NecklaceData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(string uuid, EquipmentTier tier, int level)
        {
            return new MilitaryNecklace(uuid, this, tier, level);
        }
    }
}