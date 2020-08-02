using System;
using TreniniDotNet.Common.Data;
using Xunit;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public abstract class RepositoryUnitTests<TRepository> : IClassFixture<SqliteDatabaseFixture>
    {
        protected DatabaseTestHelpers Database { get; }
        
        protected IUnitOfWork UnitOfWork { get; }

        // Repository under test
        protected TRepository Repository { get; }

        protected RepositoryUnitTests(
            SqliteDatabaseFixture fixture,
            Func<IUnitOfWork, TRepository> builder)
        {
            UnitOfWork = fixture.UnitOfWork;
            Repository = builder(UnitOfWork);
            Database = new DatabaseTestHelpers(fixture.DatabaseContext);
        }
    }
}
