using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class DepartmentCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string GetCacheKey() => "departments:list";
        public string GetSingleKey(object id) => $"departments:{id}";
        public Type CacheValueType => typeof(DepartmentModel);
    }
}
