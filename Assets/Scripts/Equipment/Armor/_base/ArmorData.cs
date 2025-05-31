using UnityEngine;

namespace ZUN
{
    public abstract class ArmorData : EquipmentData
    {
        [SerializeField] int[] initialHp;
        public int[] InitialHp => initialHp;
        [SerializeField] int[] increaseHpPerLevel;
        public int[] IncreaseHpPerLevel => increaseHpPerLevel;
        [SerializeField] float[] coefficient;
        public float[] Coefficient => coefficient;

        private void OnEnable()
        {
            Type = EquipmentType.Armor;
        }
    }
}