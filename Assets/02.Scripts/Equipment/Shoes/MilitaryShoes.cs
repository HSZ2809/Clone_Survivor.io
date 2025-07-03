namespace ZUN
{
    public class MilitaryShoes : Shoes
    {
        public MilitaryShoes(MilitaryShoesData data, EquipmentTier tier, int level) : base(data, tier, level) { }

        public override void SetTierEffect(Character character)
        {

        }
    }
}