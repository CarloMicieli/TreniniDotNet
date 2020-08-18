using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public class SqliteConnectionProvider : IConnectionProvider
    {
        private readonly DatabaseOptions _options;

        public SqliteConnectionProvider(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public DbConnection Create()
        {
            return new SqliteConnection(_options.ConnectionString);
        }
    }
}
