using UnityEngine;

namespace ZUN
{
    public abstract class ShoesData : EquipmentData
    {
        [SerializeField] int[] initialHp;
        [SerializeField] int[] increaseHpPerLevel;

        public int[] InitialHp => initialHp;
        public int[] IncreaseHpPerLevel => increaseHpPerLevel;
    }
}