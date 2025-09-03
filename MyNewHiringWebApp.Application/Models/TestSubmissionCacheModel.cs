using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class TestSubmissionCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int CandidateId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public double? Score { get; set; }

        public string GetCacheKey() => "testSubmissions:list";
        public string GetSingleKey(object id) => $"testSubmissions:{id}";
        public Type CacheValueType => typeof(TestSubmissionCacheModel);
    }
}
