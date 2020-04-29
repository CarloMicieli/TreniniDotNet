using System;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Shops
{
    public class ShopsRepositoryTests : RepositoryUnitTests<IShopsRepository>
    {
        public ShopsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IShopsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new ShopsRepository(databaseContext, new ShopsFactory(clock, new GuidSource()));

        [Fact]
        public async Task ShopsRepository_AddAsync_ShouldInsertNewShop()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            var shop = new FakeShop();

            var id = await Repository.AddAsync(shop);

            id.Should().Be(shop.ShopId);

            Database.Assert.RowInTable(Tables.Shops)
                .WithPrimaryKey(new
                {
                    shop_id = shop.ShopId.ToGuid()
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

            var shop = new FakeShop();

            Database.Arrange.WithOneShop(shop);

            bool exists = await Repository.ExistsAsync(shop.Slug);
            bool notExists = await Repository.ExistsAsync(Slug.Empty);

            exists.Should().BeTrue();
            notExists.Should().BeFalse();
        }

        [Fact]
        public async Task ShopsRepository_GetBySlugAsync_ShouldReturnTheShop()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            var shop = new FakeShop();

            Database.Arrange.WithOneShop(shop);

            var exists = await Repository.GetBySlugAsync(shop.Slug);
            var notExists = await Repository.GetBySlugAsync(Slug.Empty);

            exists.Should().NotBeNull();
            exists.ShopId.Should().Be(shop.ShopId);
            exists.Slug.Should().Be(shop.Slug);
            exists.Name.Should().Be(shop.Name);

            notExists.Should().BeNull();
        }

        [Fact]
        public async Task ShopsRepository_GetShopInfoBySlugAsync_ShouldReturnTheShopInfo()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            var shop = new FakeShop();

            Database.Arrange.WithOneShop(shop);

            var exists = await Repository.GetShopInfoBySlugAsync(shop.Slug);
            var notExists = await Repository.GetShopInfoBySlugAsync(Slug.Empty);

            exists.Should().NotBeNull();
            exists.ShopId.Should().Be(shop.ShopId);
            exists.Slug.Should().Be(shop.Slug);
            exists.Name.Should().Be(shop.Name);

            notExists.Should().BeNull();
        }

        [Fact]
        public async Task ShopsRepository_GetShopsAsync_ShouldReturnTheShops()
        {
            Database.Setup.TruncateTable(Tables.Shops);

            Database.Arrange.InsertMany(Tables.Shops, 20, id =>
            {
                return new
                {
                    shop_id = Guid.NewGuid(),
                    name = $"Test shop {id}",
                    slug = Slug.Of($"Test shop {id}"),
                    created = DateTime.UtcNow,
                    version = 1
                };
            });

            var shops = await Repository.GetShopsAsync(new Page(0, 5));

            shops.Should().NotBeNull();
            shops.Should().HaveCount(6);
        }

    }
}
