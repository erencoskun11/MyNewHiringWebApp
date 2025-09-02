using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class JobApplicationCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int JobPositionId { get; set; }
        public DateTime AppliedAt { get; set; }
        public string Status { get; set; } = null!;

        public string GetCacheKey() => "jobApplications:list";
        public string GetSingleKey(object id) => $"jobApplications:{id}";
        public Type CacheValueType => typeof(JobApplicationModel);
    }
}
