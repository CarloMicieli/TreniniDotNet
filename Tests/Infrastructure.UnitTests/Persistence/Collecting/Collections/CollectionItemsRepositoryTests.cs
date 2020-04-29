using System;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
    public class CollectionItemsRepositoryTests : RepositoryUnitTests<ICollectionItemsRepository>
    {
        private ICollection TestCollection { get; }

        public CollectionItemsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
            TestCollection = new FakeCollection();

            Database.Setup.TruncateTable(Tables.Collections);

            Database.Arrange.InsertOne(Tables.Collections, new
            {
                collection_id = TestCollection.CollectionId.ToGuid(),
                owner = TestCollection.Owner.Value,
                created = TestCollection.CreatedDate.ToDateTimeUtc(),
                version = 1
            });
        }

        private static ICollectionItemsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new CollectionItemsRepository(databaseContext, new CollectionsFactory(clock, new GuidSource()));

        [Fact]
        public async Task CollectionItemsRepository_Add_ShouldInsertNewCollectionItem()
        {
            Database.Setup.TruncateTable(Tables.CollectionItems);

            var item = new FakeCollectionItem();

            var id = await Repository.AddItemAsync(TestCollection.CollectionId, item);

            id.Should().Be(item.ItemId);

            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    item_id = item.ItemId.ToGuid()
                })
                .WithValues(new
                {
                    collection_id = TestCollection.CollectionId.ToGuid(),
                    catalog_item_id = item.CatalogItem.CatalogItemId.ToGuid(),
                    catalog_item_slug = item.CatalogItem.Slug.Value,
                    price = item.Price.Amount,
                    currency = item.Price.Currency.Code,
                    condition = item.Condition.ToString(),
                    shop_id = item.PurchasedAt.ShopId.ToGuid()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CollectionItemsRepository_EditItemAsync_ShouldEditItems()
        {
            var item = new FakeCollectionItem();

            ArrangeDatabaseWithOneCollectionItem(item);

            var modifiedItem = item.With(
                Condition: Condition.PreOwned,
                Notes: "My modified notes");

            await Repository.EditItemAsync(TestCollection.CollectionId, modifiedItem);

            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    item_id = item.ItemId.ToGuid()
                })
                .WithValues(new
                {
                    collection_id = TestCollection.CollectionId.ToGuid(),
                    catalog_item_id = item.CatalogItem.CatalogItemId.ToGuid(),
                    catalog_item_slug = item.CatalogItem.Slug.Value,
                    condition = modifiedItem.Condition.ToString(),
                    notes = modifiedItem.Notes
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CollectionItemsRepository_ItemExistsAsync_ShouldCheckItemExistence()
        {
            var item = new FakeCollectionItem();

            ArrangeDatabaseWithOneCollectionItem(item);

            var exists = await Repository.ItemExistsAsync(
                TestCollection.CollectionId,
                item.ItemId);

            exists.Should().BeTrue();

            var dontExists = await Repository.ItemExistsAsync(
                TestCollection.CollectionId,
                CollectionItemId.NewId());

            dontExists.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionItemsRepository_RemoveItemAsync_ShouldRemoveTheItem()
        {
            var item = new FakeCollectionItem();

            ArrangeDatabaseWithOneCollectionItem(item);

            var removed = new LocalDate(2019, 11, 25);

            await Repository.RemoveItemAsync(
                TestCollection.CollectionId,
                item.ItemId,
                removed);

            Database.Assert.RowInTable(Tables.CollectionItems)
                .WithPrimaryKey(new
                {
                    item_id = item.ItemId.ToGuid()
                })
                .WithValues(new
                {
                    removed_date = removed.ToDateTimeUnspecified()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CollectionItemsRepository_GetItemByIdAsync_ShouldReturnCollectionItem()
        {
            var item = new FakeCollectionItem();

            ArrangeDatabaseWithOneCollectionItem(item);

            var result = await Repository.GetItemByIdAsync(TestCollection.CollectionId, item.ItemId);

            result.Should().NotBeNull();
            result.ItemId.Should().Be(item.ItemId);
            result.CatalogItem.Should().NotBeNull();
            result.PurchasedAt.Slug.Should().NotBeNull();
        }

        private void ArrangeDatabaseWithOneCollectionItem(ICollectionItem item)
        {
            Database.Setup.TruncateTable(Tables.CollectionItems);
            Database.Setup.TruncateTable(Tables.Shops);

            Database.Arrange.InsertOne(Tables.Shops, new
            {
                shop_id = item.PurchasedAt.ShopId.ToGuid(),
                slug = item.PurchasedAt.Slug.Value,
                name = item.PurchasedAt.Name,
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(Tables.CollectionItems, new
            {
                item_id = item.ItemId.ToGuid(),
                collection_id = TestCollection.CollectionId.ToGuid(),
                catalog_item_id = item.CatalogItem.CatalogItemId.ToGuid(),
                catalog_item_slug = item.CatalogItem.Slug.Value,
                price = item.Price.Amount,
                currency = item.Price.Currency.Code,
                condition = item.Condition.ToString(),
                shop_id = item.PurchasedAt.ShopId.ToGuid()
            });
        }
    }
}
