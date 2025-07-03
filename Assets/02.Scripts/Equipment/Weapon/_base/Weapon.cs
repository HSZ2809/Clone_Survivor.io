namespace ZUN
{
    public abstract class Weapon : Equipment
    {
        new public WeaponData Data { get; private set; }

        public Weapon(WeaponData data, EquipmentTier tier, int level) : base(data, tier, level) => Data = data;
    }
}