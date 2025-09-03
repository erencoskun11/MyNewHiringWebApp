using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Services.Caching;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class SkillCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string GetCacheKey() => "skills:list";
        public string GetSingleKey(object id) => $"skills:{id}";
        public Type CacheValueType => typeof(SkillCacheModel);
    }
}
