namespace ZUN
{
    public abstract class Belt : Equipment
    {
        new public BeltData Data { get; private set; }

        public Belt(string uuid, BeltData data, EquipmentTier tier, int level) : base(uuid,data, tier, level) => Data = data;
    }
}