using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class CandidateCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime AppliedAt { get; set; }

        public ResumeSummaryCacheModel? Resume { get; set; }


        public string GetCacheKey() => "candidates:list";
        public string GetSingleKey(object id) => $"candidates:{id}";
        public Type CacheValueType => typeof(CandidateCacheModel);
    }

    public sealed class ResumeSummaryCacheModel : ICacheKeyProvider
    {
        public string? Summary { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }

        public string GetCacheKey() => "resumeSummaries:list";
        public string GetSingleKey(object id) => $"resumeSummaries:{id}";
        public Type CacheValueType => typeof(ResumeSummaryCacheModel);
    }
}









