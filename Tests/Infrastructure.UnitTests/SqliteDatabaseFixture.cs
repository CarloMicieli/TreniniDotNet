using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using static Dapper.SqlMapper;

namespace TreniniDotNet.Infrastructure
{
    public class SqliteDatabaseFixture
    {
        private readonly Guid contextId;

        public SqliteDatabaseFixture()
        {
            this.contextId = Guid.NewGuid();

            var options = Options.Create(new DatabaseOptions
            {
                ConnectionString = $"Data Source={contextId};Mode=Memory;Cache=Shared;Version=3;Pooling=False;PRAGMA foreign_keys=ON;"
            });

            this.DatabaseContext = new SqliteDatabaseContext(options);

            RegisterTypeHandlers();

            var migrationsRunner = new SqliteMigrationsRunner(options);
            migrationsRunner.MigrateUp();
        }

        public IDatabaseContext DatabaseContext { get; }

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
