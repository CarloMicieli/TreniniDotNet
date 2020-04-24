using Dapper;
using System;
using TreniniDotNet.Infrastructure.Dapper;

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

        internal void TruncateTable(object wishlists)
        {
            throw new NotImplementedException();
        }
    }
}
