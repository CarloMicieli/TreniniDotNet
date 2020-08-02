using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops
{
    public class ShopsRepositoryTests : RepositoryUnitTests<IShopsRepository>
    {
        public ShopsRepositoryTests(SqliteDatabaseFixture fixture) 
            : base(fixture, unitOfWork => new ShopsRepository(unitOfWork))
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
            Database.Arrange.WithOneShop(shop);

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
            Database.Arrange.WithOneShop(shop);

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
            shops.Results.Should().HaveCount(6);
        }
    }
}