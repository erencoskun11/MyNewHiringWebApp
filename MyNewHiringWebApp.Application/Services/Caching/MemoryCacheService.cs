using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache) => _cache = cache;


        public Task<T?> GetAsync<T>(string key)
        {
            _cache.TryGetValue(key, out T? val);
            return Task.FromResult(val);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            var opts = new MemoryCacheEntryOptions();
            if (absoluteExpiration.HasValue) opts.SetAbsoluteExpiration(absoluteExpiration.Value);
            if (slidingExpiration.HasValue) opts.SetSlidingExpiration(slidingExpiration.Value);
            _cache.Set(key, value, opts);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<long> IncrementAsync(string key) => Task.FromResult(0L);
        public Task SubscribeInvalidationAsync(string channel, Func<string, Task> handler) => Task.CompletedTask;
        public Task PublishInvalidationAsync(string channel, string message) => Task.CompletedTask;

        
    }
}
