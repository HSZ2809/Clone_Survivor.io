using UnityEngine;

namespace ZUN
{
    public abstract class WeaponData : EquipmentData
    {
        [SerializeField] int[] initialAtk;
        public int[] InitialAtk => initialAtk;
        [SerializeField] int[] increaseAtkPerLevel;
        public int[] IncreaseAtkPerLevel => increaseAtkPerLevel;
        [SerializeField] float[] coefficient;
        public float[] Coefficient => coefficient;
        [SerializeField] ActiveSkill skillPrefab;
        public ActiveSkill SkillPrefab => skillPrefab;

        private void OnEnable()
        {
            Type = EquipmentType.Weapom;
        }
    }
}