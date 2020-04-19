using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public class WishlistsRepositoryTests : RepositoryUnitTests<IWishlistsRepository>
    {
        public WishlistsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IWishlistsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new WishlistsRepository(databaseContext, new WishlistsFactory(clock, new GuidSource()));
    }
}
