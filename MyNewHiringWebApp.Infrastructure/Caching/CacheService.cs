using Microsoft.Extensions.Caching.Memory;
using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        private static readonly ConcurrentDictionary<string, bool> _keys = new();

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache= memoryCache;
        }
        public Task<T?> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            _keys.TryRemove(key, out _);
            return Task.CompletedTask;
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var keysToRemove = _keys.Keys.Where(k=> regex.IsMatch(k)).ToList();


            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                _keys.TryRemove(key,out _);
            }
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions();
        
            if(expiration.HasValue)
                cacheOptions.AbsoluteExpirationRelativeToNow = expiration.Value;

            _memoryCache.Set(key,value, cacheOptions);

            _keys[key] = true;
            return Task.CompletedTask;
             
        
        }
    }
}
