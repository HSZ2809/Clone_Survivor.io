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
        public int Level { get; private set; }
        public int LevelupGoldCost => Level * 1000;
        public int LevelupScrollCost => (Level / 3) + 1;

        public Equipment(EquipmentData data, EquipmentTier tier)
        {
            Level = 1;
            Data = data;
            Tier = tier;
        }

        public abstract void SetTierEffect(Character character);
    }
}