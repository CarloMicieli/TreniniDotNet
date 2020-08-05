using System;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionItemsFactoryTests
    {
        private CollectionItemsFactory Factory { get; }
        private readonly CollectionItemId _expectedItemId = new CollectionItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"));

        public CollectionItemsFactoryTests()
        {
            Factory = new CollectionItemsFactory(
                FakeGuidSource.NewSource(_expectedItemId));
        }

        [Fact]
        public void CollectionItemsFactory_ShouldCreateNewCollectionItems()
        {
            var item = Factory.CreateCollectionItem(
                new CatalogItemRef(CatalogSeedData.CatalogItems.NewAcme60392()),
                Condition.New,
                Price.Euro(200),
                new ShopRef(CollectingSeedData.Shops.NewModellbahnshopLippe()),
                new LocalDate(2020, 11, 25),
                "notes");

            item.Should().NotBeNull();
            item.Id.Should().Be(_expectedItemId);
            item.CatalogItem.Should().Be(new CatalogItemRef(CatalogSeedData.CatalogItems.NewAcme60392()));
            item.Condition.Should().Be(Condition.New);
            item.Price.Should().Be(Price.Euro(200));
            item.PurchasedAt.Should().Be(new ShopRef(CollectingSeedData.Shops.NewModellbahnshopLippe()));
            item.AddedDate.Should().Be(new LocalDate(2020, 11, 25));
            item.RemovedDate.Should().BeNull();
            item.Notes.Should().Be("notes");
        }
    }
}
