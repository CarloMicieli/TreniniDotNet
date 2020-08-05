using System;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistItemsFactoryTests
    {
        private WishlistItemsFactory Factory { get; }
        private readonly WishlistItemId _expectedItemId = new WishlistItemId(new Guid("fb9a54b3-9f5e-451a-8f1f-e8a921d953af"));

        public WishlistItemsFactoryTests()
        {
            Factory = new WishlistItemsFactory(
                FakeGuidSource.NewSource(_expectedItemId));
        }

        [Fact]
        public void WishlistItemsFactory_ShouldCreateNewWishlistItems()
        {
            var item = Factory.CreateWishlistItem(
                new CatalogItemRef(CatalogSeedData.CatalogItems.NewAcme60392()),
                Priority.High,
                new LocalDate(2020, 11, 25),
                Price.Euro(250),
                "notes");

            item.Should().NotBeNull();
            item.Id.Should().Be(_expectedItemId);
            item.CatalogItem.Should().Be(CatalogSeedData.CatalogItems.NewAcme60392());
            item.Priority.Should().Be(Priority.High);
            item.AddedDate.Should().Be(new LocalDate(2020, 11, 25));
            item.RemovedDate.Should().BeNull();
            item.Price.Should().Be(Price.Euro(250));
            item.Notes.Should().Be("notes");
        }
    }
}
