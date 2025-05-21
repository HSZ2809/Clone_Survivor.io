namespace ZUN
{
    public abstract class Armor : Equipment
    {
        new public ArmorData Data { get; private set; }

        public Armor(ArmorData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}