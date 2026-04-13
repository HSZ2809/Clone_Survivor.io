using System.Collections.Generic;

namespace ZUN
{
    public interface IRepository<TValue, TKey>
    {
        bool TryGet(TKey key, out TValue value);
        bool Contains(TKey key);
        IReadOnlyCollection<TValue> GetAll();
    }
}