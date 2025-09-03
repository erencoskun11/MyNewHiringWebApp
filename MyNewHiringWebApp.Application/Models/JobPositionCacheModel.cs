using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class JobPositionCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }

        public string GetCacheKey() => "jobPositions:list";
        public string GetSingleKey(object id) => $"jobPositions:{id}";
        public Type CacheValueType => typeof(JobPositionCacheModel);
    }
}
