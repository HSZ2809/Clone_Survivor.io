namespace ZUN
{
    public abstract class Necklace : Equipment
    {
        new public NecklaceData Data { get; private set; }

        public Necklace(NecklaceData data, EquipmentTier tier, int level) : base(data, tier, level) => Data = data;
    }
}