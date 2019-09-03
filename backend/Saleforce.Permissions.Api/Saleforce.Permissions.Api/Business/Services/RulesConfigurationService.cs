using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Saleforce.Permissions.Api.Core.Services;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Business.Services
{
    public class RulesConfigurationService : IRulesConfigurationService
    {
        private readonly IMemoryCache _memoryCache;
        private const string RoleRulesCacheKey = "Role_Rules_Cache_Key";


        public RulesConfigurationService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<RoleRulesView> GetRulesAsync(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(RoleRulesCacheKey, out RoleRulesView roleRules))
            {
                return roleRules;
            }

            roleRules = await GetRulesFromFileAsync(cancellationToken);
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(120),
                SlidingExpiration = TimeSpan.FromMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            _memoryCache.Set(RoleRulesCacheKey, roleRules, cacheOptions);
            return roleRules;
        }

        private static async Task<RoleRulesView> GetRulesFromFileAsync(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "role.rules.json");
            var json = await File.ReadAllTextAsync(filePath, cancellationToken);
            return JsonConvert.DeserializeObject<RoleRulesView>(json);
        }
    }
}