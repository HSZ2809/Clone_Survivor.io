using System;
using UnityEngine;

namespace ZUN
{
    public interface IGameEntityFactory
    {
        bool TryCreateEquipment(EquipmentInfo info, out Equipment equipment);
        Equipment CreateEquipment(EquipmentInfo info);
        bool TryCreateItem(ItemInfo info, out Item item);
        Item CreateItem(ItemInfo info);
    }
}