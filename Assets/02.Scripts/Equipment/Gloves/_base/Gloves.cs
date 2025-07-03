namespace ZUN
{
    public abstract class Gloves : Equipment
    {
        new public GlovesData Data { get; private set; }

        public Gloves(GlovesData data, EquipmentTier tier, int level) : base(data, tier, level) => Data = data;
    }
}