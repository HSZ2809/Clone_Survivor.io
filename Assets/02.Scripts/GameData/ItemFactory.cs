using System;

namespace ZUN
{
    public sealed class ItemFactory : IEntityFactory<ItemInfo, Item>
    {
        private readonly IRepository<ItemData, string> _repository;

        public ItemFactory(IRepository<ItemData, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool TryCreate(ItemInfo info, out Item entity)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            entity = null;

            if (string.IsNullOrWhiteSpace(info.Uid))
                return false;

            if (!_repository.TryGet(info.Uid, out var data))
                return false;

            entity = data.Create(info.Amount);
            return true;
        }
    }
}