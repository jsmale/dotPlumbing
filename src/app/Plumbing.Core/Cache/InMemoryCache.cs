using System;
using System.Collections.Generic;
using System.Linq;
using Plumbing.Extensions;
using Plumbing.Utility;

namespace Plumbing.Cache
{
    public class InMemoryCache : ICache
    {
        readonly IDateTimeService dateTimeService;

        public InMemoryCache(IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService;
            cache = new Dictionary<string, CacheItem>();
        }

        public static TimeSpan DefaultTimeout = TimeSpan.FromMinutes(15);

        class CacheItem
        {
            public object Value;
            public DateTime DateAccessed;
            public TimeSpan Timeout;
        }

        readonly IDictionary<string, CacheItem> cache;

        public T Get<T>(string key)
        {
            var item = GetItem(key);
            return item != null ? (T)item.Value : default(T);
        }

        CacheItem GetItem(string key)
        {
            if (!cache.ContainsKey(key))
            {
                return null;
            }

            var item = cache[key];
            var now = dateTimeService.Now();
            if (now > item.DateAccessed + item.Timeout)
            {
                Remove(key);
                return null;
            }

            item.DateAccessed = now;
            return item;
        }

        public object Get(string key)
        {
            var item = GetItem(key);
            return item != null ? item.Value : null;
        }

        public void Set<T>(T value, string key)
        {
            Set(value, key, DefaultTimeout);
        }

        public void Set<T>(T value, string key, TimeSpan timeout)
        {
            cache[key] = new CacheItem
            {
                DateAccessed = dateTimeService.Now(),
                Timeout = timeout,
                Value = value
            };
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void ClearExpiredItems()
        {
            var now = dateTimeService.Now();
            var expired = cache
                .Where(x => (x.Value.DateAccessed + x.Value.Timeout) < now)
                .ToArray();
            expired.Each(item => Remove(item.Key));
        }
    }
}