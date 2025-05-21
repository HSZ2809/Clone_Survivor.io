namespace ZUN
{
    public abstract class Belt : Equipment
    {
        new public BeltData Data { get; private set; }

        public Belt(BeltData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}