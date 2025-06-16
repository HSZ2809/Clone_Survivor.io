namespace ZUN
{
    public class MilitaryUniform : Armor
    {
        public MilitaryUniform(MilitaryUniformData data, EquipmentTier tier) : base(data, tier) { }

        public override void SetTierEffect(Character character)
        {
            
        }
    }
}