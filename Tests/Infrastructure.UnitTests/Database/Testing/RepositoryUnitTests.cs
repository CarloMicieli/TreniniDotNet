using NodaTime;
using NodaTime.Testing;
using System;
using TreniniDotNet.Infrastracture.Dapper;
using Xunit;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public abstract class RepositoryUnitTests<TRepository> : IClassFixture<SqliteDatabaseFixture>
    {
        protected DatabaseTestHelpers Database { get; }

        // Repository under test
        protected TRepository Repository { get; }

        protected RepositoryUnitTests(SqliteDatabaseFixture fixture, Func<IDatabaseContext, IClock, TRepository> builder)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 9, 0));
            Database = new DatabaseTestHelpers(fixture.DatabaseContext);

            Repository = builder(fixture.DatabaseContext, fakeClock);
        }
    }
}
