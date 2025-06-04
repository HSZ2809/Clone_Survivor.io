using UnityEngine;

namespace ZUN
{
    public abstract class WeaponData : EquipmentData
    {
        [SerializeField] ActiveSkill skillPrefab;
        public ActiveSkill SkillPrefab => skillPrefab;

        private void OnEnable()
        {
            Type = EquipmentType.Weapon;
        }
    }
}