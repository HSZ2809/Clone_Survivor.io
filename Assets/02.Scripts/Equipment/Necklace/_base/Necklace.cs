namespace ZUN
{
    public abstract class Necklace : Equipment
    {
        new public NecklaceData Data { get; private set; }

        public Necklace(string uuid, NecklaceData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) => Data = data;
    }
}