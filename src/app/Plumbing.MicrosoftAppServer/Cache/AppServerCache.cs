using System;
using Microsoft.ApplicationServer.Caching;
using Plumbing.Cache;

namespace Plumbing.MicrosoftAppServer.Cache
{
    public class AppServerCache : ICache
    {
        readonly DataCache cache;

        public AppServerCache(string cacheName)
        {
            cache = (new DataCacheFactory()).GetCache(cacheName);
        }

        public T Get<T>(string key)
        {
            return (T) Get(key);
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public void Set<T>(T value, string key)
        {
            cache.Put(key, value);
        }

        public void Set<T>(T value, string key, TimeSpan timeout)
        {
            cache.Put(key, value, timeout);
        }

        public void Remove(string key)
        {
            if (cache.Get(key) == null) return;

            cache.Remove(key);
        }
    }
}