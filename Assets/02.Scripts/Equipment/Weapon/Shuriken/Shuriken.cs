namespace ZUN
{
    public class Shuriken : Weapon
    {
        public Shuriken(string uuid, ShurikenData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

        public override void SetTierEffect(Character character) { }
    }
}