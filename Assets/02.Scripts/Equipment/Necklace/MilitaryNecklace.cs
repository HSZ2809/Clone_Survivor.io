using UnityEngine;
using ZUN;

public class MilitaryNecklace : Necklace
{
    public MilitaryNecklace(string uuid, MilitaryNecklaceData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) { }

    public override void SetTierEffect(Character character)
    {

    }
}
