using System;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Infrastructure.Dapper;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
    public class CollectionsRepositoryTests : CollectionRepositoryUnitTests<ICollectionsRepository>
    {
        public CollectionsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static ICollectionsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new CollectionsRepository(databaseContext, new CollectionsFactory(clock, new GuidSource()));

        [Fact]
        public async Task CollectionsRepository_Add_ShouldInsertNewCollections()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var newCollection = new FakeCollection();
            var id = await Repository.AddAsync(newCollection);

            id.Should().Be(newCollection.CollectionId);

            Database.Assert.RowInTable(Tables.Collections)
                .WithPrimaryKey(new
                {
                    collection_id = newCollection.CollectionId.ToGuid()
                })
                .WithValues(new
                {
                    owner = newCollection.Owner.Value,
                    //created = Instant.FromUtc(2019, 11, 25, 9, 0).ToDateTimeUtc(),
                    version = newCollection.Version,
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceByOwner()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            Database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = Guid.NewGuid(),
                owner = "George",
                created = DateTime.UtcNow,
                last_modified = DateTime.UtcNow,
                version = 2
            });

            bool exists = await Repository.ExistsAsync(new Owner("George"));
            bool dontExist = await Repository.ExistsAsync(new Owner("Not found"));

            exists.Should().BeTrue();
            dontExist.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceById()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var id = Guid.NewGuid();
            var owner = new Owner("George");

            Database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = id,
                owner = "George",
                created = DateTime.UtcNow,
                last_modified = DateTime.UtcNow,
                version = 2
            });

            bool exists = await Repository.ExistsAsync(owner, new CollectionId(id));
            bool dontExist = await Repository.ExistsAsync(owner, new CollectionId(Guid.NewGuid()));

            exists.Should().BeTrue();
            dontExist.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_GetIdByOwnerAsync_ReturnsCollectionIdFromOwner()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var id = Guid.NewGuid();

            Database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = id,
                owner = "George",
                created = DateTime.UtcNow,
                last_modified = DateTime.UtcNow,
                version = 2
            });

            CollectionId? found = await Repository.GetIdByOwnerAsync(new Owner("George"));
            CollectionId? notFound = await Repository.GetIdByOwnerAsync(new Owner("Not found"));

            found.HasValue.Should().BeTrue();
            found.Value.Should().Be(new CollectionId(id));
            notFound.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_GetByOwnerAsync_ReturnsCollectionByOwner()
        {
            Database.Setup.TruncateTable(Tables.CollectionItems);
            Database.Setup.TruncateTable(Tables.Collections);

            ArrangeCatalogData();

            var expectedId = new CollectionId(Guid.NewGuid());
            var expectedOwner = new Owner("George");

            Database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = expectedId.ToGuid(),
                owner = expectedOwner.Value,
                created = DateTime.UtcNow,
                last_modified = DateTime.UtcNow,
                version = 2
            });

            Database.Arrange.InsertOne(Tables.CollectionItems, new
            {
                item_id = Guid.NewGuid(),
                collection_id = expectedId.ToGuid(),
                catalog_item_id = Acme_123456.CatalogItemId.ToGuid(),
                catalog_item_slug = Acme_123456.Slug.Value,
                condition = Condition.New.ToString(),
                price = 210M,
                currency = "EUR"
            });

            Database.Arrange.InsertOne(Tables.CollectionItems, new
            {
                item_id = Guid.NewGuid(),
                collection_id = expectedId.ToGuid(),
                catalog_item_id = Acme_123457.CatalogItemId.ToGuid(),
                catalog_item_slug = Acme_123457.Slug.Value,
                condition = Condition.New.ToString(),
                price = 190M,
                currency = "EUR"
            });

            ICollection found = await Repository.GetByOwnerAsync(expectedOwner);
            ICollection notFound = await Repository.GetByOwnerAsync(new Owner("Not found"));

            notFound.Should().BeNull();
            found.Should().NotBeNull();
            found.CollectionId.Should().Be(expectedId);
            found.Owner.Should().Be(expectedOwner);
            found.Items.Should().HaveCount(2);
        }
    }
}
