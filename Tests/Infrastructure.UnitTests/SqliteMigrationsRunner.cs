using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using TreniniDotNet.Infrastracture.Dapper;
using TreniniDotNet.Infrastracture.Persistence.Migrations;

namespace TreniniDotNet.Infrastructure
{
    public sealed class SqliteMigrationsRunner
    {
        public IServiceProvider ServiceProvider { get; }

        public SqliteMigrationsRunner(IOptions<DatabaseOptions> options)
        {
            this.ServiceProvider = CreateServices(options.Value.ConnectionString);
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
                .BuildServiceProvider(false);
        }

        public void MigrateUp()
        {
            var runner = ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
