namespace ZUN
{
    public class Shuriken : Weapon
    {
        public Shuriken(ShurikenData data, EquipmentTier tier, int level) : base(data, tier, level) { }

        public override void SetTierEffect(Character character)
        {
            //if (character ==null) return;

            //if (Tier >= EquipmentTier.Rare)
            //    Debug.Log("Shuriken : SetTierEffect, Rare");
            //if (Tier >= EquipmentTier.Elite)
            //    Debug.Log("Shuriken : SetTierEffect, Elite");
            //if (Tier >= EquipmentTier.Legend)
            //    Debug.Log("Shuriken : SetTierEffect, Legend");
        }
    }
}