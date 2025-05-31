using UnityEngine;

namespace ZUN
{
    public abstract class ShoesData : EquipmentData
    {
        [SerializeField] int[] initialHp;
        public int[] InitialHp => initialHp;
        [SerializeField] int[] increaseHpPerLevel;
        public int[] IncreaseHpPerLevel => increaseHpPerLevel;
        [SerializeField] float[] coefficient;
        public float[] Coefficient => coefficient;

        private void OnEnable()
        {
            Type = EquipmentType.Shoes;
        }
    }
}