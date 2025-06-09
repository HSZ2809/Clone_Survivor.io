namespace ZUN
{
    public class Shotgun : Weapon
    {
        public Shotgun(ShotgunData data, EquipmentTier tier) : base(data, tier) { }

        public override void SetTierEffect(Character character)
        {
            return;
        }
    }
}