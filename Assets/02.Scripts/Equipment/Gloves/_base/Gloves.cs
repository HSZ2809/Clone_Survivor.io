namespace ZUN
{
    public abstract class Gloves : Equipment
    {
        new public GlovesData Data { get; private set; }

        public Gloves(string uuid, GlovesData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) => Data = data;
    }
}