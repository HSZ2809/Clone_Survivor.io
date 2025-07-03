namespace ZUN
{
    public class Shotgun : Weapon
    {
        public Shotgun(ShotgunData data, EquipmentTier tier, int level) : base(data, tier, level) { }

        public override void SetTierEffect(Character character)
        {
            return;
        }
    }
}