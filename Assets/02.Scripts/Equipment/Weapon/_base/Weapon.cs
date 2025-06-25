namespace ZUN
{
    public abstract class Weapon : Equipment
    {
        new public WeaponData Data { get; private set; }

        public Weapon(WeaponData data, EquipmentTier tier) : base(data, tier) => Data = data;
    }
}