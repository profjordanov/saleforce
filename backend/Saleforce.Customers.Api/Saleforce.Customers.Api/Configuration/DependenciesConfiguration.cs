using System;
using System.Linq;
using System.Reflection;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;
using Saleforce.Common.EventSourcing.Business;
using Saleforce.Common.EventSourcing.Core;
using Saleforce.Common.Hateoas.Business;
using Saleforce.Common.Hateoas.Core;
using Saleforce.Customers.Api.Entities;
using Saleforce.Customers.Api.Events;

namespace Saleforce.Customers.Api.Configuration
{
    public static class DependenciesConfiguration
    {
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

                options.Connection(connectionString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
                options.Events.DatabaseSchemaName = schemaName;
                options.DatabaseSchemaName = schemaName;

                options.Events.InlineProjections.AggregateStreamsWith<Customer>();
                //options.Events.InlineProjections.Add(new TabViewProjection());

                var events = typeof(CustomerRegistered)
                    .Assembly
                    .GetTypes()
                    .Where(t => typeof(IEvent).IsAssignableFrom(t))
                    .ToList();

                options.Events.AddEventTypes(events);
            });

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());

            return services;
        }
    }
}