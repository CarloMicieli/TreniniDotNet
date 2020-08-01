using System;
using System.Linq;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Infrastructure.Persistence.Migrations;

namespace TreniniDotNet.Infrastructure.Persistence.DependencyInjection
{
    public static class FluentMigratorExtensions
    {
        public static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<IDatabaseMigration>>();
            logger.LogInformation("Running database migration");

            // REMARKS: IDatabaseMigration is scoped, without an actual request we need to fake one
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var migration = scope.ServiceProvider.GetRequiredService<IDatabaseMigration>();
            migration.Up();

            return app;
        }
        
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

        public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            return services.AddMigrations(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanMigrationsIn(typeof(InitialMigration).Assembly);
            });
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