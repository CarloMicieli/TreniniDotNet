using System.Data.Common;
using System.Data.SQLite;
using Microsoft.Extensions.Options;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public class SqliteDatabaseContext : IDatabaseContext
    {
        private readonly DatabaseOptions _options;

        public SqliteDatabaseContext(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public DbConnection NewConnection()
        {
            return new SQLiteConnection(_options.ConnectionString);
        }
    }
}
