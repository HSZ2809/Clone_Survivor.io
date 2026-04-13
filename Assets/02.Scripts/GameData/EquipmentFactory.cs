using System;

namespace ZUN
{
    public sealed class EquipmentFactory : IEntityFactory<EquipmentInfo, Equipment>
    {
        private readonly IRepository<EquipmentData, string> _repository;

        public EquipmentFactory(IRepository<EquipmentData, string> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool TryCreate(EquipmentInfo info, out Equipment entity)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            entity = null;

            if (string.IsNullOrWhiteSpace(info.Id))
                return false;

            if (!_repository.TryGet(info.Id, out var data))
                return false;

            entity = data.Create(info.Uuid ?? Guid.NewGuid().ToString(), info.Tier, info.Level);
            return true;
        }
    }
}