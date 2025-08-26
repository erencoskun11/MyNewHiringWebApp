using Microsoft.Extensions.Caching.Memory;
using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Services.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, byte> _keys = new();

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
            _keys.TryAdd(key, 0);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            _keys.TryAdd(key, 0);
            return Task.CompletedTask;
        }

        public Task<long> IncrementAsync(string key) => Task.FromResult(0L);
        public Task SubscribeInvalidationAsync(string channel, Func<string, Task> handler) => Task.CompletedTask;
        public Task PublishInvalidationAsync(string channel, string message) => Task.CompletedTask;

        public Task RemoveByPatternAsync(string pattern)
        {
            // basit wildcard destek (pattern sonu "*" varsayımı)
            if (pattern.EndsWith("*"))
            {
                var prefix = pattern.TrimEnd('*');
                var keys = _keys.Keys.Where(k => k.StartsWith(prefix)).ToList();
                foreach (var k in keys)
                {
                    _cache.Remove(k);
                    _keys.TryRemove(k, out _);
                }
            }
            else
            {
                // tam eşleşme
                _cache.Remove(pattern);
                _keys.TryRemove(pattern, out _);
            }

            return Task.CompletedTask;
        }
    }
    }
