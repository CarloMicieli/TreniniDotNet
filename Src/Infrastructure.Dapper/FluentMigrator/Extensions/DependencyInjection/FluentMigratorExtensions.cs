using System;
using System.Linq;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Infrastracture.FluentMigrator.Extensions.DependencyInjection;

namespace TreniniDotNet.Infrastructure.Persistence.Migrations
{
    public static class FluentMigratorExtensions
    {
        public static IServiceCollection ReplaceMigrations(this IServiceCollection services, Action<MigrationOptions> options)
        {
            var descriptors = services
                .Where(d =>
                    d.ServiceType == typeof(IMigrationProcessor) ||
                    d.ServiceType == typeof(IMigrationGenerator) ||
                    d.ServiceType == typeof(IDatabaseMigration))
                .ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            return AddMigrations(services, options);
        }

        public static IServiceCollection AddMigrations(this IServiceCollection services, Action<MigrationOptions> options)
        {
            MigrationOptions migrationOptions = new MigrationOptions();
            options?.Invoke(migrationOptions);

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder => builder.ConfigureRunner(migrationOptions)
                                                   .ScanIn(migrationOptions.Assemblies).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddScoped<IDatabaseMigration, DatabaseMigration>();

            return services;
        }
    }


}
