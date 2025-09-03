using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class SubmittedAnswerCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public int TestSubmissionId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionIndex { get; set; }
        public string? AnswerText { get; set; }

        public string GetCacheKey() => "submittedAnswers:list";
        public string GetSingleKey(object id) => $"submittedAnswers:{id}";
        public Type CacheValueType => typeof(SubmittedAnswerCacheModel);
    }
}
