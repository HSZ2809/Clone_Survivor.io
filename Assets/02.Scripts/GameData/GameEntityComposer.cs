using System;

namespace ZUN
{
    public class GameEntityComposer
    {
        private readonly GameDataService _service;

        public GameEntityComposer(IGameDataAssets assets)
        {
            if (assets == null)
                throw new ArgumentNullException(nameof(assets));

            var equipmentData = assets.GetEquipmentData();
            var itemData = assets.GetItemData();

            var equipmentRepository = new InMemoryRepository<EquipmentData, string>(equipmentData, x => x.Id);
            var itemRepository = new InMemoryRepository<ItemData, string>(itemData, x => x.Id);

            var equipmentFactory = new EquipmentFactory(equipmentRepository);
            var itemFactory = new ItemFactory(itemRepository);

            _service = new GameDataService(equipmentFactory, itemFactory);
        }

        public bool TryCreateEquipment(EquipmentInfo info, out Equipment equipment)
        {
            return _service.TryCreateEquipment(info, out equipment);
        }

        public bool TryCreateItem(ItemInfo info, out Item item)
        {
            return _service.TryCreateItem(info, out item);
        }
    }
}
