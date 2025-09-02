using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class InterviewerCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string GetCacheKey() => "interviewers:list";
        public string GetSingleKey(object id) => $"interviewers:{id}";
        public Type CacheValueType => typeof(InterviewerModel);
    }
}
