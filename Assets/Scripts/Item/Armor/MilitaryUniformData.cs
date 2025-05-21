using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "MilitaryUniformData", menuName = "Scriptable Objects/MilitaryUniformData")]
    public class MilitaryUniformData : ArmorData
    {
        public override Equipment Create(EquipmentTier tier)
        {
            return new MilitaryUniform(this, tier);
        }
    }
}