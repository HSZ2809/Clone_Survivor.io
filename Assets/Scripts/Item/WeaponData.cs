using UnityEngine;

namespace ZUN
{
    public abstract class WeaponData : EquipmentData
    {
        [SerializeField] int[] initialAtk;
        [SerializeField] int[] increaseAtkPerLevel;
        [SerializeField] ActiveSkill skillPrefab;

        public int[] InitialAtk => initialAtk;
        public int[] IncreaseAtkPerLevel => increaseAtkPerLevel;
        public ActiveSkill SkillPrefab => skillPrefab;
    }
}