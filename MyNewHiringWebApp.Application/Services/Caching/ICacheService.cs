using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpration = null);
        Task RemoveAsync(string key);
        Task<long> IncrementAsync(string key);
        Task SubscribeInvalidationAsync(string channel, Func<string, Task> handler);
        Task PublishInvalidationAsync(string channel, string message);
        Task RemoveByPatternAsync(string pattern); // wildcard destekli



    }
}
