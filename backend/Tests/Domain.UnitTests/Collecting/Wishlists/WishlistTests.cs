using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistTests
    {
        [Fact]
        public void Wishlist_ShouldCreateNewValues()
        {
            var id = WishlistId.NewId();
            var wishlist = new Wishlist(
                id,
                new Owner("George"),
                "First list",
                Visibility.Private,
                new Budget(1000M, "EUR"),
                new List<WishlistItem>(),
                Instant.FromUtc(1988, 11, 25, 10, 30),
                null,
                1);

            wishlist.Should().NotBeNull();
            wishlist.Id.Should().Be(id);
            wishlist.Owner.Should().Be(new Owner("George"));
            wishlist.Slug.Should().Be(Slug.Of("george-first-list"));
            wishlist.ListName.Should().Be("First list");
            wishlist.Budget.Should().Be(new Budget(1000M, "EUR"));
            wishlist.Visibility.Should().Be(Visibility.Private);
            wishlist.Items.Should().HaveCount(0);
            wishlist.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 10, 30));
            wishlist.ModifiedDate.Should().BeNull();
            wishlist.Version.Should().Be(1);
        }

        [Fact]
        public void Wishlist_AddItem_ShouldAddNewItems()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var catalogItem = new CatalogItemRef(CatalogSeedData.CatalogItems.NewAcme60392());

            var item = new WishlistItem(
                WishlistItemId.NewId(),
                catalogItem,
                Priority.High,
                new LocalDate(2020, 11, 25),
                null,
                Price.Euro(123),
                "My notes");
            wishlist.AddItem(item);

            wishlist.Count.Should().Be(2);
            wishlist.Items.Should().HaveCount(2);
        }

        [Fact]
        public void Wishlist_RemoveItem_ShouldRemoveItems()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var item = wishlist.Items.First();

            wishlist.RemoveItem(item.Id, new LocalDate(2020, 11, 25));

            wishlist.Count.Should().Be(0);
        }

        [Fact]
        public void Wishlist_Contains_ShouldCheckIfContainsCatalogItem()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var contains1 = wishlist.Contains(wishlist.Items.First().CatalogItem);
            var contains2 = wishlist.Contains(
                new CatalogItemRef(CatalogSeedData.CatalogItems.NewBemo1252125()));

            contains1.Should().BeTrue();
            contains2.Should().BeFalse();
        }

        [Fact]
        public void Wishlist_Count_ShouldCountTheItems()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var count = wishlist.Count;

            count.Should().Be(1);
        }

        [Fact]
        public void Wishlist_With_ShouldModifyWishlist()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var modified = wishlist.With(
                listName: "modified list name",
                visibility: Visibility.Public);

            modified.Should().NotBeSameAs(wishlist);
            modified.ListName.Should().Be("modified list name");
            modified.Visibility.Should().Be(Visibility.Public);
        }

        [Fact]
        public void Wishlist_FindItemById_ShouldSearchItemsById()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var itemId = wishlist.Items.First().Id;

            var item1 = wishlist.FindItemById(wishlist.Items.First().Id);
            var item2 = wishlist.FindItemById(WishlistItemId.NewId());

            item1.Should().NotBeNull();
            item1?.Id.Should().Be(itemId);

            item2.Should().BeNull();
        }
    }
}
