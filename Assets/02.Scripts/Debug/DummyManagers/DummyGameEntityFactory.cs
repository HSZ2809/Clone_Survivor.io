namespace ZUN
{
    public class DummyGameEntityFactory : IGameEntityFactory
    {
        public bool TryCreateEquipment(EquipmentInfo info, out Equipment equipment)
        {
            equipment = null;
            return false;
        }

        public Equipment CreateEquipment(EquipmentInfo info) => null;

        public bool TryCreateItem(ItemInfo info, out Item item)
        {
            item = null;
            return false;
        }

        public Item CreateItem(ItemInfo info) => null;
    }
}
