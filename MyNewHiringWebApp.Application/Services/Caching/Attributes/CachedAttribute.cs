using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CachedAttribute : Attribute
    {
        public string CacheKey { get; }
        public int DurationSeconds { get; }

        public CachedAttribute(string CacheKey,int durationSeconds =600)
        {
            CacheKey = CacheKey;
            DurationSeconds = durationSeconds;
        }


    }
}
