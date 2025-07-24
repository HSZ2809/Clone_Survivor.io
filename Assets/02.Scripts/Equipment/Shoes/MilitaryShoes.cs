namespace ZUN
{
    public class MilitaryShoes : Shoes
    {
        public MilitaryShoes(string uuid, MilitaryShoesData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

        public override void SetTierEffect(Character character)
        {

        }
    }
}