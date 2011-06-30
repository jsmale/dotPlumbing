using System;

namespace Plumbing.Cache
{
    public interface ICache
    {
        T Get<T>(string key);
        object Get(string key);
        void Set<T>(T value, string key);
        void Set<T>(T value, string key, TimeSpan timeout);
        void Remove(string key);
    }
}