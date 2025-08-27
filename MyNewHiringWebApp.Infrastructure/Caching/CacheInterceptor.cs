using Castle.DynamicProxy;
using MyNewHiringWebApp.Application.Services.Caching;
using MyNewHiringWebApp.Application.Services.Caching.Attributes;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Caching
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheService _cache;
        public CacheInterceptor(ICacheService cache) => _cache = cache;

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var cachedAttr = method.GetCustomAttribute<CachedAttribute>();
            if (cachedAttr != null)
            {
                var key = BuildCacheKey(cachedAttr.CacheKey, invocation.Arguments);
                var returnType = method.ReturnType;
                if (typeof(Task).IsAssignableFrom(returnType))
                {
                    HandleAsyncCaching(invocation, key, cachedAttr.DurationSeconds).GetAwaiter().GetResult();
                    return;
                }
                invocation.Proceed();
                return;
            }

            var invalidateAttrs = method.GetCustomAttributes<InvalidateCacheAttribute>().ToArray();
            if (invalidateAttrs.Length > 0)
            {
                invocation.Proceed();
                var returnType = method.ReturnType;
                if (typeof(Task).IsAssignableFrom(returnType))
                {
                    var task = (Task)invocation.ReturnValue!;
                    task.GetAwaiter().GetResult();
                }
                foreach (var a in invalidateAttrs)
                {
                    var pattern = FormatPattern(a.Pattern, invocation.Arguments);
                    _cache.RemoveByPatternAsync(pattern).GetAwaiter().GetResult();
                }
                return;
            }

            invocation.Proceed();
        }

        private static string BuildCacheKey(string template, object?[] args)
        {
            try
            {
                return string.Format(template, args);
            }
            catch
            {
                var argStr = string.Join(",", args.Select(a => a?.ToString() ?? "null"));
                return $"{template}:{argStr}";
            }
        }

        private static string FormatPattern(string template, object?[] args)
        {
            try
            {
                return string.Format(template, args);
            }
            catch
            {
                return template;
            }
        }

        private async Task HandleAsyncCaching(IInvocation invocation, string key, int durationSeconds)
        {
            var returnType = invocation.Method.ReturnType;
            var genericT = returnType.IsGenericType ? returnType.GetGenericArguments()[0] : null;
            if (genericT == null)
            {
                invocation.Proceed();
                var t = (Task)invocation.ReturnValue!;
                await t;
                return;
            }

            var method = typeof(ICacheService).GetMethod(nameof(ICacheService.GetAsync))!;
            var genericGet = method.MakeGenericMethod(genericT);
            var cachedTask = (Task)genericGet.Invoke(_cache, new object[] { key })!;
            await cachedTask.ConfigureAwait(false);
            var cachedResultProp = cachedTask.GetType().GetProperty("Result");
            var cachedResult = cachedResultProp?.GetValue(cachedTask);

            if (cachedResult != null)
            {
                var tFromResult = typeof(Task).GetMethod("FromResult")!.MakeGenericMethod(genericT).Invoke(null, new[] { cachedResult });
                invocation.ReturnValue = tFromResult;
                return;
            }

            invocation.Proceed();
            var executedTask = (Task)invocation.ReturnValue!;
            await executedTask.ConfigureAwait(false);
            var resultProp = executedTask.GetType().GetProperty("Result");
            var resultValue = resultProp?.GetValue(executedTask);

            if (resultValue != null)
            {
                var setMethod = typeof(ICacheService).GetMethod(nameof(ICacheService.SetAsync))!;
                var genericSet = setMethod.MakeGenericMethod(genericT);
                genericSet.Invoke(_cache, new[] { key, resultValue, TimeSpan.FromSeconds(durationSeconds) });
            }
        }
    }
}

