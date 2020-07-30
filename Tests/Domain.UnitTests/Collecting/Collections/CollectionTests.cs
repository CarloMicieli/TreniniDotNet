using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionTests
    {
        [Fact]
        public void Collection_ShouldCreateNewValues()
        {
            var collection = new Collection(
                CollectionId.NewId(),
                new Owner("George"),
                null,
                new List<CollectionItem>(),
                Instant.FromUtc(1988, 11, 25, 10, 30),
                null,
                1);

            collection.Should().NotBeNull();
            collection.Owner.Should().Be(new Owner("George"));
            collection.Items.Should().HaveCount(0);
            collection.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 10, 30));
            collection.ModifiedDate.Should().BeNull();
            collection.Version.Should().Be(1);
        }

        [Fact]
        public void Collection_Add_ShouldAddNewItems()
        {
            var collection = CollectingSeedData.Collections.GeorgeCollection();

            var item = new CollectionItem(
                CollectionItemId.NewId(),
                CatalogSeedData.CatalogItems.Bemo_1252125(),
                Condition.New,
                Price.Euro(210),
                CollectingSeedData.Shops.ModellbahnshopLippe(),
                new LocalDate(2020, 11, 25),
                null,
                null
            );

            collection.AddItem(item);

            collection.Items.Should().HaveCount(2);
        }

        [Fact]
        public void Collection_UpdateItem_ShouldModifyItems()
        {
            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var item = collection.Items.First();

            var modifiedItem = item.With(price: Price.Euro(999));
            collection.UpdateItem(modifiedItem);

            collection.Items.First(it => it.Id == modifiedItem.Id)
                .Price.Should().Be(Price.Euro(999));
        }

        [Fact]
        public void Collection_FindItemById_ShouldFindCollectionItemsByTheirId()
        {
            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var item = collection.Items.First();

            var item1 = collection.FindItemById(item.Id);
            var item2 = collection.FindItemById(CollectionItemId.NewId());

            item1.Should().NotBeNull();
            item1.Should().Be(item);

            item2.Should().BeNull();
        }
    }
}
