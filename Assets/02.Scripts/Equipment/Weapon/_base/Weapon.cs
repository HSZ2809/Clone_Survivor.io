namespace ZUN
{
    public abstract class Weapon : Equipment
    {
        new public WeaponData Data { get; private set; }

        public Weapon(string uuid, WeaponData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) => Data = data;
    }
}