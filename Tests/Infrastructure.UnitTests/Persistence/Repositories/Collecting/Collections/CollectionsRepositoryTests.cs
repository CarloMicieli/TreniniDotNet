using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
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
        public async Task CollectionsRepository_AddAsync_ShouldInsertCollectionWithoutItems()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var collection = CollectionWithoutItems();

            var id = await Repository.AddAsync(collection);
            await UnitOfWork.SaveAsync();
            
            id.Should().Be(collection.Id);

            Database.Assert.RowInTable(Tables.Collections)
                .WithPrimaryKey(new
                {
                    collection_id = collection.Id.ToGuid()
                })
                .WithValues(new
                {
                    owner = collection.Owner.Value,
                    notes = collection.Notes
                    //created = Instant.FromUtc(2019, 11, 25, 9, 0).ToDateTimeUtc(),
                })
                .ShouldExists();
        }
       
        [Fact]
        public async Task CollectionsRepository_AddAsync_ShouldInsertCollectionItems()
        {
            Database.Setup.TruncateTable(Tables.Collections);

            var collection = CollectionWithTwoItems();
            var firstItem = collection.Items.First();
            var secondItem = collection.Items.Last();
            
            var id = await Repository.AddAsync(collection);
            await UnitOfWork.SaveAsync();
            
            id.Should().Be(collection.Id);
           
            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    collection_item_id = firstItem.Id.ToGuid()
                })
                .WithValues(new
                {
                    collection_id = collection.Id.ToGuid(),
                    catalog_item_id = firstItem.CatalogItem.Id.ToGuid(),
                    notes = firstItem.Notes,
                    price = firstItem.Price.Amount,
                    currency = firstItem.Price.Currency,
                    condition = firstItem.Condition.ToString(),
                    purchased_at = firstItem.PurchasedAt?.Id.ToGuid()
                })
                .ShouldExists();
            
            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    collection_item_id = secondItem.Id.ToGuid()
                })
                .WithValues(new
                {
                    collection_id = collection.Id.ToGuid(),
                    catalog_item_id = secondItem.CatalogItem.Id.ToGuid(),
                    notes = secondItem.Notes,
                    price = secondItem.Price.Amount,
                    currency = secondItem.Price.Currency,
                    condition = secondItem.Condition.ToString(),
                    purchased_at = secondItem.PurchasedAt?.Id.ToGuid()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CollectionsRepository_ExistsAsync_ShouldCheckCollectionExistenceByOwner()
        {
            Database.ArrangeWithCollection(CollectionWithTwoItems());

            var exists = await Repository.ExistsAsync(new Owner("George"));
            var dontExist = await Repository.ExistsAsync(new Owner("Not found"));

            exists.Should().BeTrue();
            dontExist.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsRepository_GetByOwnerAsync_ReturnsCollectionByOwner()
        {
            var collection = CollectionWithTwoItems();
            var expectedId = collection.Id;
            var expectedOwner = collection.Owner;

            Database.ArrangeWithCollection(collection);

            var found = await Repository.GetByOwnerAsync(expectedOwner);
            var notFound = await Repository.GetByOwnerAsync(new Owner("Not found"));

            notFound.Should().BeNull();
            
            found.Should().NotBeNull();
            found?.Id.Should().Be(expectedId);
            found?.Owner.Should().Be(expectedOwner);
            found?.Items.Should().HaveCount(2);
        }
        
        [Fact]
        public async Task CollectionsRepository_UpdateAsync_ShouldUpdateCollections()
        {
            var collection = CollectionWithTwoItems();
            Database.ArrangeWithCollection(collection);

            var modifiedCollection = collection.With(notes: "Modified notes");
        
            await Repository.UpdateAsync(modifiedCollection);
            await UnitOfWork.SaveAsync();
        
            Database.Assert.RowInTable(Tables.Collections)
                .WithPrimaryKey(new
                {
                    collection_id = modifiedCollection.Id.ToGuid()
                })
                .WithValues(new
                {
                    notes = modifiedCollection.Notes
                })
                .ShouldExists();
        }
        
        [Fact]
        public async Task CollectionsRepository_UpdateAsync_ShouldEditCollectionItems()
        {
            var collection = CollectionWithTwoItems();
            var item = collection.Items.First();
            
            Database.ArrangeWithCollection(collection);
        
            var modifiedItem = item.With(
                condition: Condition.PreOwned,
                notes: "My modified notes");
            
            collection.UpdateItem(modifiedItem);
        
            await Repository.UpdateAsync(collection);
            await UnitOfWork.SaveAsync();
        
            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    collection_item_id = item.Id.ToGuid()
                })
                .WithValues(new
                {
                    collection_id = collection.Id.ToGuid(),
                    catalog_item_id = item.CatalogItem.Id.ToGuid(),
                    condition = modifiedItem.Condition.ToString(),
                    notes = modifiedItem.Notes
                })
                .ShouldExists();
        }
        
        [Fact]
        public async Task CollectionsRepository_UpdateAsync_ShouldRemoveCollectionItems()
        {
            var collection = CollectionWithTwoItems();
            var item = collection.Items.First();
            
            Database.ArrangeWithCollection(collection);
        
            collection.RemoveItem(item.Id);
        
            await Repository.UpdateAsync(collection);
            await UnitOfWork.SaveAsync();
        
            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    collection_item_id = item.Id.ToGuid()
                })
               .ShouldNotExists();
        }

        private static Collection CollectionWithoutItems()
        {
            return CollectingSeedData.Collections.New()
                .Id(new Guid("54262f02-7cde-40fc-bb30-03e38067b170"))
                .Owner(new Owner("George"))
                .Notes("My awesome collection")
                .CreatedDate(Instant.FromUtc(2020, 11, 25, 10, 30))
                .Build();
        }

        private static Collection CollectionWithTwoItems()
        {
            return CollectingSeedData.Collections.New()
                .Id(new Guid("c99e92f6-e055-4102-a30a-958a33bc5286"))
                .Owner(new Owner("George"))
                .Item(builder =>
                {
                    builder
                        .ItemId(new Guid("957fcce6-63a9-4377-9288-8b62059ea006"))
                        .CatalogItem(CatalogSeedData.CatalogItems.NewAcme60392())
                        .Condition(Condition.New)
                        .Notes("My item notes")
                        .Price(Price.Euro(299M))
                        .AddedDate(new LocalDate(2020, 11, 25))
                        .Shop(CollectingSeedData.Shops.NewModellbahnshopLippe())
                        .Build();
                })
                .Item(builder =>
                {
                    builder
                        .ItemId(new Guid("d764c0d8-24cc-49cb-9ced-542936865677"))
                        .CatalogItem(CatalogSeedData.CatalogItems.NewAcme60458())
                        .Condition(Condition.New)
                        .Notes("My second item notes")
                        .Price(Price.Euro(199M))
                        .AddedDate(new LocalDate(2020, 11, 25))
                        .Shop(CollectingSeedData.Shops.NewModellbahnshopLippe())
                        .Build();
                })
                .Notes("My awesome collection")
                .CreatedDate(Instant.FromUtc(2020, 11, 25, 10, 30))
                .Build();
        }
    }
}