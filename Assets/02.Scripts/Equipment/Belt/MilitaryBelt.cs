namespace ZUN
{
    public class MilitaryBelt : Belt
    {
        public MilitaryBelt(string uuid, MilitaryBeltData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

        public override void SetTierEffect(Character character)
        {

        }
    }
}