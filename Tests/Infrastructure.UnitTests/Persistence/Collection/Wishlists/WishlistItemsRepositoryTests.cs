using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public class WishlistItemsRepositoryTests : RepositoryUnitTests<IWishlistItemsRepository>
    {
        public WishlistItemsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IWishlistItemsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new WishlistItemsRepository(databaseContext, new WishlistsFactory(clock, new GuidSource()));
    }
}
