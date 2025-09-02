using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class CandidateSkillCacheModel : ICacheKeyProvider
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
        public SkillCacheModel? Skill { get; set; }

        public string GetCacheKey() => "candidateSkills:list";
        public string GetSingleKey(object id) => $"candidateSkills:{id}"; // id could be composite; pass $"{candidateId}:{skillId}" when calling
        public Type CacheValueType => typeof(CandidateSkillModel);
    }
}
