using Dapper;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    // Database setup / preconditions management
    public sealed class DatabaseSetup
    {
        public IConnectionProvider ConnectionProvider { get; }

        public DatabaseSetup(IConnectionProvider connectionProvider) =>
            ConnectionProvider = connectionProvider;

        public void TruncateTable(string tableName)
        {
            using var connection = ConnectionProvider.Create();
            connection.Execute($"DELETE FROM {tableName}", new { });
        }
    }
}
