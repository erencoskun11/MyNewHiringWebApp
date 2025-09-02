using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Models
{
    public sealed class TestQuestionCacheModel : ICacheKeyProvider
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? OptionsJson { get; set; }

        public string GetCacheKey() => "testQuestions:list";
        public string GetSingleKey(object id) => $"testQuestions:{id}";
        public Type CacheValueType => typeof(TestQuestionModel);
    }
}
