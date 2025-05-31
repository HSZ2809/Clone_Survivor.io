using UnityEngine;

namespace ZUN
{
    public abstract class GlovesData : EquipmentData
    {
        [SerializeField] int[] initialAtk;
        public int[] InitialAtk => initialAtk;
        [SerializeField] int[] increaseAtkPerLevel;
        public int[] IncreaseAtkPerLevel => increaseAtkPerLevel;
        [SerializeField] float[] coefficient;
        public float[] Coefficient => coefficient;

        private void OnEnable()
        {
            Type = EquipmentType.Gloves;
        }
    }
}