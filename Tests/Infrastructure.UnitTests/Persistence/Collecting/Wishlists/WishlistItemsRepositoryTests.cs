using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using FluentAssertions;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Wishlists
{
    public class WishlistItemsRepositoryTests : RepositoryUnitTests<IWishlistItemsRepository>
    {
        public WishlistItemsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IWishlistItemsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new WishlistItemsRepository(databaseContext, new WishlistsFactory(clock, new GuidSource()), clock);


        [Fact]
        public async Task WishlistItemsRepository_AddItemAsync_ShouldCreateNewWishlistItem()
        {
            var wishlist = new FakeWishlist();
            var newItem = new FakeWishlistItem();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            var newId = await Repository.AddItemAsync(wishlist.WishlistId, newItem);

            newId.Should().NotBeNull();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.ItemId.ToGuid()
                })
                .AndValues(new
                {
                    // catalog_item_id = newItem.CatalogItem.CatalogItemId.ToGuid(),
                    catalog_item_slug = newItem.CatalogItem.Slug.Value
                })
                .ShouldExists();
        }

        [Fact]
        public async Task WishlistItemsRepository_DeleteItemAsync_ShouldRemoveWishlistItem()
        {
            IWishlistItem newItem = new FakeWishlistItem();
            IWishlist wishlist = new FakeWishlist().With(Items: ImmutableList.Create(newItem));

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            await Repository.DeleteItemAsync(wishlist.WishlistId, newItem.ItemId);

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.ItemId.ToGuid()
                })
                .ShouldNotExists();
        }

        [Fact]
        public async Task WishlistItemsRepository_EditItemAsync_ShouldModifyWishlistItem()
        {
            var newItem = new FakeWishlistItem();
            var wishlist = new FakeWishlist().With(
                Items: ImmutableList.Create((IWishlistItem)newItem));

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            IWishlistItem modified = newItem.With(
                Priority: Priority.Low,
                Price: Money.Euro(199M),
                Notes: "Modified notes");

            await Repository.EditItemAsync(wishlist.WishlistId, modified);

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.ItemId.ToGuid()
                })
                .AndValues(new
                {
                    priority = Priority.Low.ToString(),
                    price = 199M,
                    currency = "EUR",
                    notes = "Modified notes"
                })
                .ShouldExists();
        }

        [Fact]
        public async Task WishlistItemsRepository_GetItemIdByCatalogRefAsync_ShouldFindItemIdFromCatalogItem()
        {
            var newItem = new FakeWishlistItem();
            var wishlist = new FakeWishlist().With(
                Items: ImmutableList.Create((IWishlistItem)newItem));

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            var itemId1 = await Repository.GetItemIdByCatalogRefAsync(
                wishlist.WishlistId,
                CatalogRef.Of(Guid.NewGuid(), "acme-123456"));
            var itemId2 = await Repository.GetItemIdByCatalogRefAsync(
                wishlist.WishlistId,
                CatalogRef.Of(Guid.NewGuid(), "acme-not-found"));

            itemId1.Should().NotBeNull();
            itemId2.Should().BeNull();
        }

        [Fact]
        public async Task WishlistItemsRepository_GetItemByIdAsync_ShouldReturnWishlistItem()
        {
            var newItem = new FakeWishlistItem();
            var wishlist = new FakeWishlist().With(
                Items: ImmutableList.Create((IWishlistItem)newItem));

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            var item = await Repository.GetItemByIdAsync(wishlist.WishlistId, newItem.ItemId);

            item.Should().NotBeNull();
        }
    }
}
