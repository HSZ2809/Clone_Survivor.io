namespace ZUN
{
    public class MilitaryShoes : Shoes
    {
        public MilitaryShoes(MilitaryShoesData data, EquipmentTier tier) : base(data, tier) { }

        public override void SetTierEffect(Character character)
        {

        }
    }
}