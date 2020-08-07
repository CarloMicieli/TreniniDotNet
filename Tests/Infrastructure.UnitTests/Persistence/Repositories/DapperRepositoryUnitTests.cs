using System;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories
{
    public abstract class DapperRepositoryUnitTests<TRepository>
    {
        protected TRepository Repository { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected DatabaseTestHelpers Database { get; }

        protected DapperRepositoryUnitTests(Func<IUnitOfWork, TRepository> builder)
        {
            var config = new RepositoryDatabaseConfig();

            Database = new DatabaseTestHelpers(config.ConnectionProvider);
            UnitOfWork = new DapperUnitOfWork(config.ConnectionProvider);

            Repository = builder(UnitOfWork);
        }
    }
}