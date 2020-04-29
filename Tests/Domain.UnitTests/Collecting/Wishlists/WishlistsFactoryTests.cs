using System;
using FluentAssertions;
using NodaMoney;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistsFactoryTests
    {
        private Guid ExpectedId = new Guid("15455823-fca5-42ab-b4dc-99ed2f4e3dac");

        private WishlistsFactory Factory { get; }

        public WishlistsFactoryTests()
        {
            Factory = new WishlistsFactory(
                FakeClock.FromUtc(1988, 11, 25, 9, 0, 0),
                FakeGuidSource.NewSource(ExpectedId));
        }

        [Fact]
        public void WishlistsFactory_NewWishlist_ShouldCreateNewValues()
        {
            string listName = "My first list";

            var owner = new Owner("George");
            var slug = Slug.Of(listName);

            var wishlist = Factory.NewWishlist(owner, slug, listName, Visibility.Private);

            wishlist.Should().NotBeNull();
            wishlist.WishlistId.Should().Be(new WishlistId(ExpectedId));
            wishlist.Owner.Should().Be(new Owner(owner));
            wishlist.ListName.Should().Be(listName);
            wishlist.Visibility.Should().Be(Visibility.Private);
            wishlist.Items.Should().HaveCount(0);
            wishlist.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 9, 0, 0));
            wishlist.Version.Should().Be(1);
        }

        [Fact]
        public void WishlistsFactory_NewWishlistItem_ShouldCreateNewItems()
        {
            var catalogRef = CatalogRef.Of(Guid.NewGuid(), "acme-123456");

            var wishlistItem = Factory.NewWishlistItem(
                catalogRef,
                Priority.Normal,
                new LocalDate(2020, 11, 25),
                Money.Euro(199),
                "My notes");

            wishlistItem.Should().NotBeNull();
            wishlistItem.ItemId.Should().Be(new WishlistItemId(ExpectedId));
            wishlistItem.CatalogItem.Should().Be(catalogRef);
            wishlistItem.Notes.Should().Be("My notes");
            wishlistItem.Priority.Should().Be(Priority.Normal);
            wishlistItem.AddedDate.Should().Be(new LocalDate(2020, 11, 25));
            wishlistItem.Price.Should().Be(Money.Euro(199));
        }
    }
}