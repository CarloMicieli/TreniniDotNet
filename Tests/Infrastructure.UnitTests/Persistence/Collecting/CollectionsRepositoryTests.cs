using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Infrastructure.Persistence.Collecting;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Collecting
{
    public class CollectionsRepositoryTests : EfRepositoryUnitTests<CollectionsRepository>
    {
        public CollectionsRepositoryTests()
            : base(context => new CollectionsRepository(context))
        {
        }

        [Fact]
        public async Task CollectionsRepository_AddAsync_ShouldInsertNewCollections()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);

                var id = await repo.AddAsync(collection);
                await context.SaveChangesAsync();

                id.Should().Be(collection.Id);
            }

            await using (var context = NewDbContext())
            {
                var exists = await context.Collections.AnyAsync(it => it.Id == collection.Id);
                exists.Should().BeTrue();
            }
        }

        [Fact]
        public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceById()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exists1 = await repo.ExistsAsync(collection.Id);
            var exists2 = await repo.ExistsAsync(CollectionId.NewId());

            exists1.Should().BeTrue();
            exists2.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceByOwner()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var exists1 = await repo.ExistsAsync(collection.Owner);
            var exists2 = await repo.ExistsAsync(new Owner("not found"));

            exists1.Should().BeTrue();
            exists2.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_GetByIdAsync_ReturnsCollectionsByTheirId()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var collection1 = await repo.GetByIdAsync(collection.Id);
            var collection2 = await repo.GetByIdAsync(CollectionId.NewId());

            collection1.Should().NotBeNull();
            collection1?.Items.Should().NotBeNull();
            collection1?.Items.Should().HaveCount(1);

            var firstItem = collection1?.Items.First();
            // firstItem?.PurchasedAt.Should().NotBeNull();
            firstItem?.CatalogItem.Should().NotBeNull();
            firstItem?.CatalogItem.Brand.Should().NotBeNull();
            firstItem?.CatalogItem.Scale.Should().NotBeNull();
            firstItem?.CatalogItem?.RollingStocks.Should().HaveCount(1);
            firstItem?.CatalogItem?.RollingStocks.First().Railway.Should().NotBeNull();

            collection2.Should().BeNull();
        }

        [Fact]
        public async Task CollectionsRepository_GetByOwnerAsync_ReturnsCollectionsByTheirId()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var collection1 = await repo.GetByOwnerAsync(new Owner("George"));
            var collection2 = await repo.GetByOwnerAsync(new Owner("not found"));

            collection1.Should().NotBeNull();
            collection1?.Items.Should().NotBeNull();
            collection1?.Items.Should().HaveCount(1);

            var firstItem = collection1?.Items.First();
            // firstItem?.PurchasedAt.Should().NotBeNull();
            firstItem?.CatalogItem.Should().NotBeNull();
            firstItem?.CatalogItem?.RollingStocks.Should().HaveCount(1);

            collection2.Should().BeNull();
        }
    }
}
