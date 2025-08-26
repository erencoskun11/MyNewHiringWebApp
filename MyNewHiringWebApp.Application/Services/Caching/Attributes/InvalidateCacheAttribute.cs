using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]

    public sealed class InvalidateCacheAttribute :Attribute
    {
        public string Pattern { get; set; }

        public InvalidateCacheAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
