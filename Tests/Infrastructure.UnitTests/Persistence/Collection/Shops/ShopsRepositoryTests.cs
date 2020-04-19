using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shops
{
    public class ShopsRepositoryTests : RepositoryUnitTests<IShopsRepository>
    {
        public ShopsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IShopsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new ShopsRepository(databaseContext, new ShopsFactory(clock, new GuidSource()));
    }
}
