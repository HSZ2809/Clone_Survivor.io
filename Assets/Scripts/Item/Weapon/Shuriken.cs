using UnityEngine;

namespace ZUN
{
    public class Shuriken : Weapon
    {
        public Shuriken(ShurikenData data, EquipmentTier tier) : base(data, tier) { }

        public override void SetTierEffect(Character character)
        {
            if (character ==null) return;

            /*
            if (Tier >= EquipmentTier.Legend)

            switch(Tier)
            {
                case EquipmentTier.Legend:
                    Debug.Log("Shuriken : SetTierEffect, Legend");
                case EquipmentTier.Epic:
                    Debug.Log("Shuriken : SetTierEffect, Epic");
                case EquipmentTier.Elite:
                    Debug.Log("Shuriken : SetTierEffect, Elite");
                case EquipmentTier.Rare:
                    Debug.Log("Shuriken : SetTierEffect, Rare");
                case EquipmentTier.Common:
                    Debug.Log("Shuriken : SetTierEffect, Common");
                    break;
                default:
                    break;
            }
            */
        }
    }
}