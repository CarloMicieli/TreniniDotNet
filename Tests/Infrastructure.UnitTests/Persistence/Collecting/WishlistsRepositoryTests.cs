using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Collecting;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Collecting
{
    public class WishlistsRepositoryTests : EfRepositoryUnitTests<WishlistsRepository>
    {
        public WishlistsRepositoryTests()
            : base(context => new WishlistsRepository(context))
        {
        }

        [Fact]
        public async Task WishlistsRepository_GetByIdAsync_ReturnsWishlistsByTheirId()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var wishlist1 = await repo.GetByIdAsync(wishlist.Id);
            var wishlist2 = await repo.GetByIdAsync(WishlistId.NewId());

            wishlist1.Should().NotBeNull();
            wishlist1?.Items.Should().NotBeNull();
            wishlist1?.Items.Should().HaveCount(1);

            var firstItem = wishlist1?.Items.First();
            firstItem?.CatalogItem.Should().NotBeNull();
            firstItem?.CatalogItem.Brand.Should().NotBeNull();
            firstItem?.CatalogItem.Scale.Should().NotBeNull();
            firstItem?.CatalogItem?.RollingStocks.Should().HaveCount(1);
            firstItem?.CatalogItem?.RollingStocks.First().Railway.Should().NotBeNull();

            wishlist2.Should().BeNull();
        }

        [Fact]
        public async Task WishlistsRepository_GetByOwnerAsync_ReturnsWishlistsByTheirOwner()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var wishlists1 = await repo.GetByOwnerAsync(wishlist.Owner, VisibilityCriteria.All);
            var wishlists2 = await repo.GetByOwnerAsync(new Owner("not found"), VisibilityCriteria.All);

            wishlists1.Should().NotBeNull();
            wishlists1.Should().HaveCount(1);

            wishlists2.Should().NotBeNull();
            wishlists2.Should().HaveCount(0);
        }

        [Fact]
        public async Task WishlistsRepository_CountWishlistsAsync_ShouldCountWishlistsByTheirOwner()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var count1 = await repo.CountWishlistsAsync(new Owner("George"));
            var count2 = await repo.CountWishlistsAsync(new Owner("Not found"));

            count1.Should().Be(1);
            count2.Should().Be(0);
        }

        [Fact]
        public async Task WishlistsRepository_ExistsAsync_ShouldCheckOwnerHasAlreadyWishlistWithGivenName()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exist1 = await repo.ExistsAsync(new Owner("George"), "First list");
            var exist2 = await repo.ExistsAsync(new Owner("Not found"), "First list");

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsRepository_ExistsAsync_ShouldCheckWishlistWithGiveIdAlreadyExists()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exist1 = await repo.ExistsAsync(wishlist.Id);
            var exist2 = await repo.ExistsAsync(WishlistId.NewId());

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }
    }
}
