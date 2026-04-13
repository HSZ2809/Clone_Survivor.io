using System;
using System.Collections.Generic;

namespace ZUN
{
    public sealed class InMemoryRepository<TValue, TKey> : IRepository<TValue, TKey>
    {
        private readonly Dictionary<TKey, TValue> _store;

        public InMemoryRepository(IEnumerable<TValue> values, Func<TValue, TKey> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            _store = new Dictionary<TKey, TValue>();

            if (values == null)
                return;

            foreach (var value in values)
            {
                if (value == null)
                    continue;

                var key = keySelector(value);
                if (key == null)
                    throw new ArgumentException("Key selector returned null.", nameof(keySelector));

                if (_store.ContainsKey(key))
                    throw new ArgumentException($"Duplicate key found in source data: {key}", nameof(values));

                _store.Add(key, value);
            }
        }

        public bool TryGet(TKey key, out TValue value)
        {
            return _store.TryGetValue(key, out value);
        }

        public bool Contains(TKey key)
        {
            return _store.ContainsKey(key);
        }

        public IReadOnlyCollection<TValue> GetAll()
        {
            return _store.Values;
        }
    }
}