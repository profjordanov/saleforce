using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Saleforce.Permissions.Api.Persistence.EntityFramework;

namespace Saleforce.Permissions.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        internal static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            return services.AddDbContext<PermissionsDbContext>(builder =>
                builder.UseNpgsql(connectionString));
        }
    }
}