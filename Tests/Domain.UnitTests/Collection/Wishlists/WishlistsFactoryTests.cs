using NodaTime.Testing;
using Xunit;
using FluentAssertions;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collection.ValueObjects;
using NodaTime;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public class WishlistsFactoryTests
    {
        private WishlistsFactory Factory { get; }

        public WishlistsFactoryTests()
        {
            Factory = new WishlistsFactory(
                FakeClock.FromUtc(1988, 11, 25, 9, 0, 0),
                FakeGuidSource.NewSource(new Guid("15455823-fca5-42ab-b4dc-99ed2f4e3dac")));
        }

        [Fact]
        public void WishlistsFactory_NewWishlist_ShouldCreateNewValues()
        {
            string listName = "My first list";
            string owner = "George";

            var wishlist = Factory.NewWishlist(owner, Visibility.Private, listName);

            wishlist.Should().NotBeNull();
            wishlist.WishlistId.Should().Be(new WishlistId(new Guid("15455823-fca5-42ab-b4dc-99ed2f4e3dac")));
            wishlist.Owner.Should().Be(owner);
            wishlist.ListName.Should().Be(listName);
            wishlist.Visibility.Should().Be(Visibility.Private);
            wishlist.Items.Should().HaveCount(0);
            wishlist.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 9, 0, 0));
            wishlist.Version.Should().Be(1);
        }

        [Fact]
        public void WishlistsFactory_NewWishlist_ShouldUseListNameAsSlug()
        {
            string listName = "My first list";

            var wishlist = Factory.NewWishlist("George", Visibility.Private, listName);

            wishlist.Should().NotBeNull();
            wishlist.Slug.Should().Be(Slug.Of(listName));
        }
    }
}