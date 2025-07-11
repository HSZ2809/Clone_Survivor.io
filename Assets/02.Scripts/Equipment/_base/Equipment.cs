using UnityEngine;

namespace ZUN
{
    public enum EquipmentTier
        {
            Common,
            Superior,
            Rare,
            Elite,
            Epic,
            Legend
        }

    public abstract class Equipment
    {
        public EquipmentData Data { get; private set; }
        public EquipmentTier Tier { get; private set; }
        public int Level { get; set; }
        public int LevelupGoldCost => Level * 1000;
        public int LevelupScrollCost => (Level / 3) + 1;
        public float Stat
        {
            get
            {
                float stat = 0.0f;
                stat += Data.InitialStat[(int)Tier];
                stat += Data.IncreaseStatPerLevel[(int)Tier] * Level;
                return stat;
            }
        }
        public float Coefficient
        {
            get
            {
                float coefficient = 0.0f;
                for (int i = 0; i < (int)Tier; i++)
                    coefficient = Data.Coefficient[i];
                return coefficient;
            }
        }
        public Sprite ItemSprite
        {
            get { return (Tier < EquipmentTier.Elite) ? Data.IconSprite[0] : Data.IconSprite[1]; }
        }

        public Equipment(EquipmentData data, EquipmentTier tier, int level)
        {
            Data = data;
            Tier = tier;
            Level = level;
        }

        public abstract void SetTierEffect(Character character);
    }
}