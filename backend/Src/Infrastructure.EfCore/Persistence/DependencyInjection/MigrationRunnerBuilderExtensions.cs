using FluentMigrator.Runner;

namespace TreniniDotNet.Infrastructure.Persistence.DependencyInjection
{
    internal static class MigrationRunnerBuilderExtensions
    {
        public static IMigrationRunnerBuilder ConfigureRunner(this IMigrationRunnerBuilder builder, MigrationOptions options)
        {
            return WithDriver(builder, options.DriverName)
                .WithGlobalConnectionString(options.ConnectionString);
        }

        private static IMigrationRunnerBuilder WithDriver(IMigrationRunnerBuilder builder, string? driverName)
        {
            return driverName switch
            {
                "Postgres" => builder.AddPostgres(),
                "Sqlite" => builder.AddSQLite(),
                _ => builder
            };
        }
    }
}