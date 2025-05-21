namespace ZUN
{
    public enum EquipmentTier
        {
            Common,
            Rare,
            Elite,
            Epic,
            Legend
        }

    public abstract class Equipment
    {
        public EquipmentData Data { get; private set; }
        public int Level { get; private set; }
        public EquipmentTier Tier { get; private set; }

        public Equipment(EquipmentData data, EquipmentTier tier)
        {
            Data = data;
            Tier = tier;
        }

        public abstract void SetTierEffect(Character character);
    }
}