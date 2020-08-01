using System.Reflection;

namespace TreniniDotNet.Infrastructure.Persistence.DependencyInjection
{
    public class MigrationOptions
    {
        internal string? ConnectionString { get; set; }
        internal string? DriverName { get; set; }
        internal Assembly[] Assemblies { get; set; } = System.Array.Empty<Assembly>();

        public void UsePostgres(string connectionString)
        {
            ConnectionString = connectionString;
            DriverName = "Postgres";
        }

        public void UseSqlite(string connectionString)
        {
            ConnectionString = connectionString;
            DriverName = "Sqlite";
        }

        public void ScanMigrationsIn(params Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }
    }
}