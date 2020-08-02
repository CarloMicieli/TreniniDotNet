using FluentMigrator.Runner;

namespace TreniniDotNet.Infrastracture.FluentMigrator.Extensions.DependencyInjection
{
    internal static class IMigrationRunnerBuilderExtensions
    {
        public static IMigrationRunnerBuilder ConfigureRunner(this IMigrationRunnerBuilder builder, MigrationOptions options)
        {
            return WithDriver(builder, options.DriverName)
                    .WithGlobalConnectionString(options.ConnectionString);
        }

        private static IMigrationRunnerBuilder WithDriver(IMigrationRunnerBuilder builder, string? driverName)
        {
            if ("Postgres" == driverName)
            {
                return builder.AddPostgres();
            }
            if ("Sqlite" == driverName)
            {
                return builder.AddSQLite();
            }

            return builder;
        }
    }
}
