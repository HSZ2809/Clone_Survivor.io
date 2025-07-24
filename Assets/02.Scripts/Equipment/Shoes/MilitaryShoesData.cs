using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryShoesData", menuName = "Scriptable Objects/Equipmet Data/Shoes/MilitaryShoesData")]
    public class MilitaryShoesData : ShoesData
    {
        [SerializeField] string[] tierSkillDescription = new string[5];
        public override string[] GetTierSkillDescription()
        {
            return tierSkillDescription;
        }

        public override Equipment Create(string uuid, EquipmentTier tier, int level)
        {
            return new MilitaryShoes(uuid, this, tier, level);
        }
    }
}