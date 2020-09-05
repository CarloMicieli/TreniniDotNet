using System.Data.Common;
using Microsoft.Extensions.Options;
using Npgsql;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public class NpgsqlConnectionProvider : IConnectionProvider
    {
        private readonly DatabaseOptions _options;

        public NpgsqlConnectionProvider(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public DbConnection Create()
        {
            return new NpgsqlConnection(_options.ConnectionString);
        }
    }
}
