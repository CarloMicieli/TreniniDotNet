using System;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using static Dapper.SqlMapper;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories
{
    public sealed class RepositoryDatabaseConfig
    {
        public RepositoryDatabaseConfig()
        {
            var connectionString = new SqliteConnectionStringBuilder($"Data Source={Guid.NewGuid()}")
            {
                ForeignKeys = true,
                Cache = SqliteCacheMode.Private,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            var options = Options.Create(new DatabaseOptions
            {
                ConnectionString = connectionString
            });

            ConnectionProvider = new SqliteConnectionProvider(options);

            RegisterTypeHandlers();

            var migrationsRunner = new SqliteMigrationsRunner(options);
            migrationsRunner.MigrateUp();
        }

        public IConnectionProvider ConnectionProvider { get; }

        private static void RegisterTypeHandlers()
        {
            var assembly = typeof(GuidTypeHandler).Assembly;
            var baseType = typeof(ITypeHandler);

            var typeHandlers = assembly
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t));

            foreach (var typeHandler in typeHandlers)
            {
                var iTypeHandler = Activator.CreateInstance(typeHandler) as ITypeHandler;
                var type = typeHandler?.BaseType?.GetGenericArguments()[0];
                if (type != null && iTypeHandler != null)
                {
                    SqlMapper.AddTypeHandler(type, iTypeHandler);
                }
            }
        }
    }
}
