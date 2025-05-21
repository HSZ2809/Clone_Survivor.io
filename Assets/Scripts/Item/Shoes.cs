namespace ZUN
{
    public abstract class Shoes : Equipment
    {
        new public ShoesData Data { get; private set; }

        public Shoes(ShoesData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}