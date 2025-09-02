using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class InterviewCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public int InterviewerId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
        public string Result { get; set; } = null!;

        public string GetCacheKey() => "interviews:list";
        public string GetSingleKey(object id) => $"interviews:{id}";
        public Type CacheValueType => typeof(InterviewModel);
    }
}
