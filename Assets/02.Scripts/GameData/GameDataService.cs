using System;

namespace ZUN
{
    public class GameDataService
    {
        private readonly IEntityFactory<EquipmentInfo, Equipment> _equipmentFactory;
        private readonly IEntityFactory<ItemInfo, Item> _itemFactory;

        public GameDataService(
            IEntityFactory<EquipmentInfo, Equipment> equipmentFactory,
            IEntityFactory<ItemInfo, Item> itemFactory)
        {
            _equipmentFactory = equipmentFactory ?? throw new ArgumentNullException(nameof(equipmentFactory));
            _itemFactory = itemFactory ?? throw new ArgumentNullException(nameof(itemFactory));
        }

        public bool TryCreateEquipment(EquipmentInfo info, out Equipment equipment)
        {
            return _equipmentFactory.TryCreate(info, out equipment);
        }

        public bool TryCreateItem(ItemInfo info, out Item item)
        {
            return _itemFactory.TryCreate(info, out item);
        }
    }
}