using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using static Dapper.SqlMapper;

namespace TreniniDotNet.Infrastructure
{
    public class SqliteDatabaseFixture
    {
        public SqliteDatabaseFixture()
        {
            var connectionString = new SqliteConnectionStringBuilder("Data Source=:Sharable:")
            {
                ForeignKeys = true,
                Cache = SqliteCacheMode.Private,
                Mode = SqliteOpenMode.Memory
            }.ToString();

            var options = Options.Create(new DatabaseOptions
            {
                ConnectionString = connectionString
            });

            DatabaseContext = new SqliteDatabaseContext(options);
            UnitOfWork = new DapperUnitOfWork(DatabaseContext);

            RegisterTypeHandlers();

            var migrationsRunner = new SqliteMigrationsRunner(options);
            migrationsRunner.MigrateUp();
        }

        public IDatabaseContext DatabaseContext { get; }

        public IUnitOfWork UnitOfWork { get; }

        private static void RegisterTypeHandlers()
        {
            Assembly assembly = typeof(GuidTypeHandler).Assembly;
            Type baseType = typeof(ITypeHandler);

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
