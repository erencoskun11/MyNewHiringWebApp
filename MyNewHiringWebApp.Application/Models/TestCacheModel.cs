using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class TestCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string GetCacheKey() => "tests:list";
        public string GetSingleKey(object id) => $"tests:{id}";
        public Type CacheValueType => typeof(TestCacheModel);
    }
}
