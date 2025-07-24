namespace ZUN
{
    public class Shotgun : Weapon
    {
        public Shotgun(string uuid, ShotgunData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

        public override void SetTierEffect(Character character)
        {
            return;
        }
    }
}