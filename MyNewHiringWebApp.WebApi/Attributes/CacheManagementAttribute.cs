using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNewHiringWebApp.Application.Services.Caching;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CacheManagementAttribute : ActionFilterAttribute
    {
        private readonly Type _providerType;
        private readonly CacheOperationType _opType;
        private readonly TimeSpan? _expiration;

        public CacheManagementAttribute(Type providerType, CacheOperationType opType, int ttlSeconds = 300)
        {
            _providerType = providerType ?? throw new ArgumentNullException(nameof(providerType));
            _opType = opType;
            _expiration = ttlSeconds > 0 ? TimeSpan.FromSeconds(ttlSeconds) : null;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CacheManagementAttribute>>();

            ICacheKeyProvider? provider = null;
            try
            {
                var provObj = context.HttpContext.RequestServices.GetService(_providerType);
                provider = provObj as ICacheKeyProvider
                           ?? (ICacheKeyProvider?)ActivatorUtilities.CreateInstance(context.HttpContext.RequestServices, _providerType);
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Cache key provider creation failed for {Type}", _providerType.FullName);
            }

            if (provider == null)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            var cache = context.HttpContext.RequestServices.GetService<ICacheService>();
            if (cache == null)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            try
            {
                if (_opType == CacheOperationType.Read)
                {
                    var cacheKey = TryGetId(context, out var id) ? provider.GetSingleKey(id) : provider.GetCacheKey();

                    if (!string.IsNullOrWhiteSpace(cacheKey))
                    {
                        var getMethod = typeof(ICacheService).GetMethod(nameof(ICacheService.GetAsync))!;
                        var genericGet = getMethod.MakeGenericMethod(provider.CacheValueType);
                        var task = (Task)genericGet.Invoke(cache, new object[] { cacheKey })!;
                        await task.ConfigureAwait(false);
                        var cachedValue = task.GetType().GetProperty("Result")?.GetValue(task);

                        if (cachedValue != null)
                        {
                            context.Result = new OkObjectResult(cachedValue);
                            return;
                        }
                    }

                    var executed = await next();

                    if (executed.Result is ObjectResult objRes && objRes.Value != null)
                    {
                        var cacheKeyAfter = TryGetId(context, out var idAfter) ? provider.GetSingleKey(idAfter) : provider.GetCacheKey();
                        if (!string.IsNullOrWhiteSpace(cacheKeyAfter))
                        {
                            var setMethod = typeof(ICacheService).GetMethod(nameof(ICacheService.SetAsync))!;
                            var genericSet = setMethod.MakeGenericMethod(provider.CacheValueType);
                            genericSet.Invoke(cache, new object[] { cacheKeyAfter, objRes.Value, _expiration });
                        }
                    }
                }
                else // Refresh
                {
                    var executed = await next();
                    if (executed.Exception != null || executed.ExceptionHandled) return;

                    try { await cache.RemoveAsync(provider.GetCacheKey()).ConfigureAwait(false); } catch { }

                    if (TryGetId(context, out var id))
                    {
                        var singleKey = provider.GetSingleKey(id);
                        if (executed.Result is ObjectResult o && o.Value != null)
                        {
                            var setMethod = typeof(ICacheService).GetMethod(nameof(ICacheService.SetAsync))!;
                            var genericSet = setMethod.MakeGenericMethod(provider.CacheValueType);
                            genericSet.Invoke(cache, new object[] { singleKey, o.Value, _expiration });
                        }
                        else
                        {
                            try { await cache.RemoveAsync(singleKey).ConfigureAwait(false); } catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "CacheManagement failed for {Provider} on {Path}", _providerType.FullName, context.HttpContext.Request.Path);
            }
        }

        private static bool TryGetId(ActionExecutingContext ctx, out object id)
        {
            var match = ctx.ActionArguments.FirstOrDefault(p =>
                string.Equals(p.Key, "id", StringComparison.OrdinalIgnoreCase) ||
                p.Key.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

            if (!match.Equals(default(KeyValuePair<string, object?>)) && match.Value != null)
            {
                id = match.Value!;
                return true;
            }

            id = default!;
            return false;
        }
    }
}
