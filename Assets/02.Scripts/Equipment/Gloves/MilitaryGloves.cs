namespace ZUN
{
    public class MilitaryGloves : Gloves
    {
        public MilitaryGloves(string uuid, MilitaryGlovesData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) 
        { 

        }

        public override void SetTierEffect(Character character)
        {

        }
    }
}