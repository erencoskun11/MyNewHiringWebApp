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
    public class RedisCacheService : ICacheService, IDisposable
    {
        private readonly IDatabase _db;
        private readonly IConnectionMultiplexer _redis;
        private readonly ISubscriber _sub;

        public RedisCacheService(IConnectionMultiplexer multiplexer)
        {
            _redis = multiplexer;
            _db = multiplexer.GetDatabase();
            _sub = multiplexer.GetSubscriber();
        }

        private static byte[] Serialize<T>(T item) =>
            JsonSerializer.SerializeToUtf8Bytes(item, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        private static T? Deserialize<T>(byte[]? bytes)
        {
            if (bytes == null) return default;
            return JsonSerializer.Deserialize<T>(bytes, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var val = await _db.StringGetAsync(key);
            if (val.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(val!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            var json = JsonSerializer.Serialize(value);
            if (absoluteExpiration.HasValue)
                await _db.StringSetAsync(key, json, absoluteExpiration);
            else
                await _db.StringSetAsync(key, json);
            // Note: Redis doesn't support sliding expiration natively without additional logic.
        }

        public async Task RemoveAsync(string key) => await _db.KeyDeleteAsync(key);

        public async Task<long> IncrementAsync(string key)
        {
            return await _db.StringIncrementAsync(key);
        }

        public Task SubscribeInvalidationAsync(string channel, Func<string, Task> handler)
        {
            _sub.Subscribe(channel, async (chan, msg) => await handler(msg));
            return Task.CompletedTask;
        }

        public Task PublishInvalidationAsync(string channel, string message)
        {
            return _sub.PublishAsync(channel, message);
        }

        public void Dispose() => _redis?.Dispose();
    }
}
