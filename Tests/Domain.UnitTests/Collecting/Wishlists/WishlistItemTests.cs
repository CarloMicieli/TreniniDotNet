using System.Linq;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistItemTests
    {
        [Fact]
        public void WishlistItem_ShouldCreateNewItems()
        {
            var itemId = WishlistItemId.NewId();

            var item = new WishlistItem(
                itemId,
                CatalogSeedData.CatalogItems.Acme_60392(),
                Priority.High,
                new LocalDate(2020, 11, 25),
                null,
                Price.Euro(123),
                "My notes");

            item.Id.Should().Be(itemId);
            item.CatalogItem.Should().Be(CatalogSeedData.CatalogItems.Acme_60392());
            item.Priority.Should().Be(Priority.High);
            item.AddedDate.Should().Be(new LocalDate(2020, 11, 25));
            item.Price.Should().Be(Price.Euro(123));
            item.Notes.Should().Be("My notes");
        }

        [Fact]
        public void Wishlist_With_ShouldModifyWishlistItems()
        {
            var item = CollectingSeedData.Wishlists.GeorgeFirstList().Items.First();

            var modified = item.With(
                price: Price.Euro(321),
                notes: "Modified notes");

            modified.Should().NotBeSameAs(item);
            modified.Notes.Should().Be("Modified notes");
            modified.Price.Should().Be(Price.Euro(321));
        }
    }
}
