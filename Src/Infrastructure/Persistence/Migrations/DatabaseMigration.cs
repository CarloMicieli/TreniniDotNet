using FluentMigrator.Runner;

namespace TreniniDotNet.Infrastracture.Persistence.Migrations
{
    public sealed class DatabaseMigration : IDatabaseMigration
    {
        private readonly IMigrationRunner migrationRunner;

        public DatabaseMigration(IMigrationRunner migrationRunner)
        {
            this.migrationRunner = migrationRunner;
        }

        public void Down(long version)
        {
            migrationRunner.MigrateDown(version);
        }

        public void Up()
        {
            migrationRunner.MigrateUp();
        }
    }
}
