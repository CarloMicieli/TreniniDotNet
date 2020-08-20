using FluentMigrator.Runner;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public sealed class DatabaseMigration : IDatabaseMigration
    {
        private readonly IMigrationRunner _migrationRunner;

        public DatabaseMigration(IMigrationRunner migrationRunner)
        {
            _migrationRunner = migrationRunner;
        }

        public void Down(long version)
        {
            _migrationRunner.MigrateDown(version);
        }

        public void Up()
        {
            _migrationRunner.MigrateUp();
        }
    }
}