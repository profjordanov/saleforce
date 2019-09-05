using System;
using System.Linq;
using System.Reflection;
using Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;
using Saleforce.Common.EventSourcing.Business;
using Saleforce.Common.EventSourcing.Core;
using Saleforce.Common.Hateoas.Business;
using Saleforce.Common.Hateoas.Core;
using Saleforce.Permissions.Api.Business.Services;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Core.Services;
using Saleforce.Permissions.Api.Persistence.EntityFramework;
using Saleforce.Permissions.Api.Persistence.Repositories;

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

        public static IServiceCollection AddHateoas(this IServiceCollection services)
        {
            services.AddTransient<IResourceMapper, ResourceMapper>();

            services.AddLinks(config =>
            {
                var policies = Assembly
                    .GetAssembly(typeof(DependenciesConfiguration))
                    .GetTypes()
                    .Where(t => !t.IsInterface &&
                                !t.IsAbstract &&
                                t.GetInterfaces().Any(i => i.Name == typeof(IPolicy<>).Name))
                    .Select(Activator.CreateInstance)
                    .Cast<dynamic>()
                    .ToArray();

                foreach (var policy in policies)
                {
                    config.AddPolicy(policy.PolicyConfiguration);
                }
            });

            return services;
        }

        public static IServiceCollection AddEventSourcing(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEventBus, EventBus>();

            var documentStore = DocumentStore.For(options =>
            {
                var config = configuration.GetSection("EventStore");
                var connectionString = config.GetValue<string>("ConnectionString");
                var schemaName = config.GetValue<string>("Schema");
            });

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var repositoryTypes = Assembly
                .GetAssembly(typeof(IRoleAssignmentRepository))
                .GetTypes()
                .Where(type => type.Name.EndsWith("Repository"))
                .ToArray();

            var repositoryImplementationTypes = Assembly
                .GetAssembly(typeof(RoleAssignmentRepository))
                .GetTypes()
                .Where(type => type.Name.EndsWith("Repository"))
                .ToDictionary(type => type.Name, type => type);

            foreach (var repositoryType in repositoryTypes)
            {
                var expectedImplementationName = repositoryType.Name.Substring(1);

                if (!repositoryImplementationTypes.ContainsKey(expectedImplementationName))
                {
                    throw new InvalidOperationException($"Could not find implementation for {repositoryType.FullName}.");
                }

                var implementation = repositoryImplementationTypes[expectedImplementationName];

                if (!repositoryType.IsAssignableFrom(implementation))
                {
                    throw new InvalidOperationException(
                        $"For repository {repositoryType.Name} found matching type {implementation.Name}, but it does not implement it.");
                }

                services.AddTransient(repositoryType, implementation);
            }

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleAssignmentService, RoleAssignmentService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IRulesConfigurationService, RulesConfigurationService>();

            return services;
        }
    }
}