using MyNewHiringWebApp.Application.Services.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Caching
{
    public sealed class RedisCacheService : ICacheService, IDisposable
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly IDatabase _db;
        private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public RedisCacheService(IConnectionMultiplexer mux)
        {
            _mux = mux ?? throw new ArgumentNullException(nameof(mux));
            _db = _mux.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var val = await _db.StringGetAsync(key).ConfigureAwait(false);
            if (val.IsNullOrEmpty) return default;
            try
            {
                return JsonSerializer.Deserialize<T>(val!, _jsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value, _jsonOptions);
            await _db.StringSetAsync(key, json, expiration).ConfigureAwait(false);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key).ConfigureAwait(false);
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            var endpoints = _mux.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _mux.GetServer(endpoint);
                foreach (var key in server.Keys(pattern: pattern))
                {
                    await _db.KeyDeleteAsync(key).ConfigureAwait(false);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}