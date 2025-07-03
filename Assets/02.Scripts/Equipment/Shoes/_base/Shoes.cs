namespace ZUN
{
    public abstract class Shoes : Equipment
    {
        new public ShoesData Data { get; private set; }

        public Shoes(ShoesData data, EquipmentTier tier, int level) : base(data, tier, level) => Data = data;
    }
}