namespace ZUN
{
    public class MilitaryBelt : Belt
    {
        public MilitaryBelt(MilitaryBeltData data, EquipmentTier tier, int level) : base(data, tier, level) { }

        public override void SetTierEffect(Character character)
        {

        }
    }
}