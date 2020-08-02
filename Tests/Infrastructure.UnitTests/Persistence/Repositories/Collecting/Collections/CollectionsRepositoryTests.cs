using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public class CollectionsRepositoryTests : CollectionRepositoryUnitTests<ICollectionsRepository>
    {
        public CollectionsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, unitOfWork => new CollectionsRepository(unitOfWork))
        {
        }
       
        [Fact]
        public async Task CollectionsRepository_AddAsync_ShouldInsertNewCollections()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var newCollection = FakeCollection();
            var id = await Repository.AddAsync(newCollection);

            id.Should().Be(newCollection.Id);

            Database.Assert.RowInTable(Tables.Collections)
                .WithPrimaryKey(new
                {
                    collection_id = newCollection.Id.ToGuid()
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

        // [Fact]
        // public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceById()
        // {
        //     Database.Setup.TruncateTable(Tables.Collections);
        //
        //     var id = Guid.NewGuid();
        //     var owner = new Owner("George");
        //
        //     Database.Arrange.InsertOne(Tables.Collections, new
        //     {
        //         collection_id = id,
        //         owner = "George",
        //         created = DateTime.UtcNow,
        //         last_modified = DateTime.UtcNow,
        //         version = 2
        //     });
        //
        //     bool exists = await Repository.ExistsAsync(owner, new CollectionId(id));
        //     bool dontExist = await Repository.ExistsAsync(owner, new CollectionId(Guid.NewGuid()));
        //
        //     exists.Should().BeTrue();
        //     dontExist.Should().BeFalse();
        // }

        // [Fact]
        // public async Task CollectionsRepository_GetIdByOwnerAsync_ReturnsCollectionIdFromOwner()
        // {
        //     Database.Setup.TruncateTable(Tables.Collections);
        //
        //     var id = Guid.NewGuid();
        //
        //     Database.Arrange.InsertOne(Tables.Collections, new
        //     {
        //         collection_id = id,
        //         owner = "George",
        //         created = DateTime.UtcNow,
        //         last_modified = DateTime.UtcNow,
        //         version = 2
        //     });
        //
        //     CollectionId? found = await Repository.GetIdByOwnerAsync(new Owner("George"));
        //     CollectionId? notFound = await Repository.GetIdByOwnerAsync(new Owner("Not found"));
        //
        //     found.HasValue.Should().BeTrue();
        //     found.Value.Should().Be(new CollectionId(id));
        //     notFound.HasValue.Should().BeFalse();
        // }

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
                catalog_item_id = Acme_60392.Id.ToGuid(),
                catalog_item_slug = Acme_60458.Slug.Value,
                condition = Condition.New.ToString(),
                price = 210M,
                currency = "EUR"
            });

            Database.Arrange.InsertOne(Tables.CollectionItems, new
            {
                item_id = Guid.NewGuid(),
                collection_id = expectedId.ToGuid(),
                catalog_item_id = Acme_60392.Id.ToGuid(),
                catalog_item_slug = Acme_60392.Slug.Value,
                condition = Condition.New.ToString(),
                price = 190M,
                currency = "EUR"
            });

            var found = await Repository.GetByOwnerAsync(expectedOwner);
            var notFound = await Repository.GetByOwnerAsync(new Owner("Not found"));

            notFound.Should().BeNull();
            found.Should().NotBeNull();
            found.Id.Should().Be(expectedId);
            found.Owner.Should().Be(expectedOwner);
            found.Items.Should().HaveCount(2);
        }
        
        public Collection FakeCollection() => throw new NotImplementedException();
    }
}