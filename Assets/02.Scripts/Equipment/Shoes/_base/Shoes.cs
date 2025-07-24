namespace ZUN
{
    public abstract class Shoes : Equipment
    {
        new public ShoesData Data { get; private set; }

        public Shoes(string uuid, ShoesData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) => Data = data;
    }
}