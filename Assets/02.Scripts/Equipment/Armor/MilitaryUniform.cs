namespace ZUN
{
    public class MilitaryUniform : Armor
    {
        public MilitaryUniform(string uuid, MilitaryUniformData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

        public override void SetTierEffect(Character character)
        {
            
        }
    }
}