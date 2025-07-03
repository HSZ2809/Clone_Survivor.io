using System;

namespace ZUN
{
    public abstract class Armor : Equipment
    {
        new public ArmorData Data { get; private set; }

        public Armor(ArmorData data, EquipmentTier tier, int level) : base(data, tier, level) => Data = data;
    }
}