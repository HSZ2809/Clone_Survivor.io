using UnityEngine;

namespace ZUN
{
    public abstract class GlovesData : EquipmentData
    {
        [SerializeField] int[] initialAtk;
        [SerializeField] int[] increaseAtkPerLevel;

        public int[] InitialAtk => initialAtk;
        public int[] IncreaseAtkPerLevel => increaseAtkPerLevel;
    }
}