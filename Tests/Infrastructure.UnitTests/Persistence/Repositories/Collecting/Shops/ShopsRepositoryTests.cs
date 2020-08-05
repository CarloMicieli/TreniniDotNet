using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops
{
    public class ShopsRepositoryTests : DapperRepositoryUnitTests<IShopsRepository>
    {
        public ShopsRepositoryTests()
            : base(unitOfWork => new ShopsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task ShopsRepository_AddAsync_ShouldInsertNewShop()
        {
            Database.Setup.TruncateTable(Tables.Shops);
            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();

            var id = await Repository.AddAsync(shop);
            await UnitOfWork.SaveAsync();

            id.Should().Be(shop.Id);

            Database.Assert.RowInTable(Tables.Shops)
                .WithPrimaryKey(new
                {
                    shop_id = shop.Id.ToGuid()
                })
                .AndValues(new
                {
                    name = shop.Name,
                    slug = shop.Slug.Value
                })
                .ShouldExists();
        }

        [Fact]
        public async Task ShopsRepository_ExistsAsync_ShouldCheckShopExistence()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();
            Database.ArrangeWithOneShop(shop);

            var exists = await Repository.ExistsAsync(shop.Slug);
            var notExists = await Repository.ExistsAsync(Slug.Empty);

            exists.Should().BeTrue();
            notExists.Should().BeFalse();
        }

        [Fact]
        public async Task ShopsRepository_GetBySlugAsync_ShouldReturnTheShop()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();
            Database.ArrangeWithOneShop(shop);

            var exists = await Repository.GetBySlugAsync(shop.Slug);
            var notExists = await Repository.GetBySlugAsync(Slug.Empty);

            exists.Should().NotBeNull();
            exists?.Id.Should().Be(shop.Id);
            exists?.Slug.Should().Be(shop.Slug);
            exists?.Name.Should().Be(shop.Name);

            notExists.Should().BeNull();
        }

        [Fact]
        public async Task ShopsRepository_GetShopsAsync_ShouldReturnTheShops()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            Database.Arrange.InsertMany(Tables.Shops, 20, id => new
            {
                shop_id = Guid.NewGuid(),
                name = $"Test shop {id}",
                slug = Slug.Of($"Test shop {id}"),
                created = DateTime.UtcNow,
                version = 1
            });

            var shops = await Repository.GetShopsAsync(new Page(0, 5));

            shops.Should().NotBeNull();
            shops.Results.Should().HaveCount(5);
        }

        [Fact]
        public async Task ShopsRepository_AddShopToFavouritesAsync_ShouldAddOneShopToUserFavourites()
        {
            var owner = new Owner("George");
            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();
            Database.ArrangeWithOneShop(shop);

            await Repository.AddShopToFavouritesAsync(owner, shop.Id);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.ShopFavourites)
                .WithPrimaryKey(new
                {
                    owner = owner.Value,
                    shop_id = shop.Id.ToGuid()
                })
                .ShouldExists();
        }
        
        [Fact]
        public async Task ShopsRepository_RemoveFromFavouritesAsync_ShouldRemoveTheShopFromUserFavourites()
        {
            var owner = new Owner("George");
            var shop = CollectingSeedData.Shops.NewModellbahnshopLippe();
            Database.ArrangeWithOneShop(shop);

            await Repository.RemoveFromFavouritesAsync(owner, shop.Id);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.ShopFavourites)
                .WithPrimaryKey(new
                {
                    owner = owner.Value,
                    shop_id = shop.Id.ToGuid()
                })
                .ShouldNotExists();
        }
        
        [Fact]
        public async Task ShopsRepository_GetFavouriteShopsAsync_ShouldAddOneShopToUserFavourites()
        {
            var owner = new Owner("George");
            var modellbahnshopLippe = CollectingSeedData.Shops.NewModellbahnshopLippe();
            var tecnomodel = CollectingSeedData.Shops.NewTecnomodelTreni();
            Database.ArrangeWithShopFavourites(owner, modellbahnshopLippe, tecnomodel);

            var results = await Repository.GetFavouriteShopsAsync(owner);

            results.Should().HaveCount(2);
            results.Should().Contain(modellbahnshopLippe);
            results.Should().Contain(tecnomodel);
        }
    }
}