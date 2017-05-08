using System;
using System.Runtime.Caching;

namespace JessicasAquariumMonitor.Helpers.Caching
{
    public interface ICacheProvider
    {
        TimeSpan GetDefaultTimeout();
        T GetOrAdd<T>(string key, Func<T> getFunction, TimeSpan timeout = default(TimeSpan));
        void Remove(string key);
    }

    internal sealed class CacheProvider : ICacheProvider
    {
        private readonly MemoryCache _memoryCache;

        public CacheProvider()
        {
            _memoryCache = MemoryCache.Default;
        }

        public T GetOrAdd<T>(string key, Func<T> getFunction, TimeSpan timeout = new TimeSpan())
        {
            var memoryCache = _memoryCache;

            if (memoryCache.Contains(key))
            {
                return (T) memoryCache[key];
            }

            var expiration = DateTime.Now.Add(timeout);
            var expirationOffset = new DateTimeOffset(expiration);

            var value = getFunction();

            memoryCache.AddOrGetExisting(key, value, expirationOffset);

            return value;
        }

        public void Remove(string key) => _memoryCache.Remove(key);
        public TimeSpan GetDefaultTimeout() => TimeSpan.FromHours(8);
    }
}