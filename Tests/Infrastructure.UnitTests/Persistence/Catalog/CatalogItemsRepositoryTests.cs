using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Catalog
{
    public class CatalogItemsRepositoryTests : EfRepositoryUnitTests<CatalogItemsRepository>
    {
        public CatalogItemsRepositoryTests()
            : base(context => new CatalogItemsRepository(context))
        {
        }

        [Fact]
        public async Task CatalogItemsRepository_AddAsync_ShouldCreateNewCatalogItems()
        {
            var testItem = CatalogSeedData.CatalogItems.NewAcme60392();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);

                var id = await repo.AddAsync(testItem);
                await context.SaveChangesAsync();

                id.Should().Be(testItem.Id);
            }

            await using (var context = NewDbContext())
            {
                var found = await context.CatalogItems
                    .AnyAsync(it => it.Id == testItem.Id);

                found.Should().BeTrue();
            }
        }

        [Fact]
        public async Task CatalogItemsRepository_GetBySlugAsync_ShouldFindOneCatalogItemBySlug()
        {
            var item = CatalogSeedData.CatalogItems.NewAcme60392();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var catalogItem1 = await repo.GetBySlugAsync(item.Slug);
            var catalogItem2 = await repo.GetBySlugAsync(Slug.Of("not found"));

            catalogItem1.Should().NotBeNull();
            catalogItem1?.Slug.Should().Be(item.Slug);
            catalogItem1?.Brand.Should().NotBeNull();
            catalogItem1?.Scale.Should().NotBeNull();
            catalogItem1?.RollingStocks.Should().HaveCount(1);

            catalogItem2.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemsRepository_ExistsAsync_ShouldCheckOneCatalogItemExists()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var found1 = await repo.ExistsAsync(CatalogSeedData.Brands.NewAcme(), new ItemNumber("60392"));
            var found2 = await repo.ExistsAsync(CatalogSeedData.Brands.NewAcme(), new ItemNumber("12345"));

            found1.Should().BeTrue();
            found2.Should().BeFalse();
        }

        [Fact]
        public async Task CatalogItemsRepository_GetLatestCatalogItemsAsync_ShouldReturnRecentItems()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var results = await repo.GetLatestCatalogItemsAsync(new Page(0, 2));

            results.Should().NotBeNull();
            results.Results.Should().HaveCount(2);
        }
    }
}