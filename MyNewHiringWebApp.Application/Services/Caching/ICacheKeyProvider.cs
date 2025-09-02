using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services.Caching
{
    public interface ICacheKeyProvider
    {
        string GetCacheKey();
        string GetSingleKey(object id);
        Type CacheValueType { get; }  
    }

}
