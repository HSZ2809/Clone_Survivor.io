using System;

namespace ZUN
{
    public abstract class Armor : Equipment
    {
        new public ArmorData Data { get; private set; }

        public Armor(string uuid, ArmorData data, EquipmentTier tier, int level) : base(uuid, data, tier, level) => Data = data;
    }
}