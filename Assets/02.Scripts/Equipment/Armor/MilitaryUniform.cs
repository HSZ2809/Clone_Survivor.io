namespace ZUN
{
    public class MilitaryUniform : Armor
    {
        public MilitaryUniform(MilitaryUniformData data, EquipmentTier tier, int level) : base(data, tier, level) { }

        public override void SetTierEffect(Character character)
        {
            
        }
    }
}