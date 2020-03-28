using Dapper;
using TreniniDotNet.Infrastracture.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    // Database setup / preconditions management
    public sealed class DatabaseSetup
    {
        public IDatabaseContext DatabaseContext { get; }

        public DatabaseSetup(IDatabaseContext databaseContext) =>
            DatabaseContext = databaseContext;

        public void TruncateTable(string tableName)
        {
            using var connection = DatabaseContext.NewConnection();
            connection.Execute($"DELETE FROM {tableName}", new { });
        }
    }
}
