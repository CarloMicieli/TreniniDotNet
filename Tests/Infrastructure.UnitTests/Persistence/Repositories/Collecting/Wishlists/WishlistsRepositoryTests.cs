using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public class WishlistsRepositoryTests : CollectionRepositoryUnitTests<IWishlistsRepository>
    {
        public WishlistsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, unitOfWork => new WishlistsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task WishlistsRepository_AddAsync_ShouldCreateWishlist()
        {
            Database.Setup.WithoutAnyWishlist();

            var wishlist = FakeWishlist();

            var id = await Repository.AddAsync(wishlist);
            await UnitOfWork.SaveAsync();

            id.Should().Be(wishlist.Id);

            Database.Assert.RowInTable(Tables.Wishlists)
                .WithPrimaryKey(new
                {
                    wishlist_id = id.ToGuid()
                })
                .AndValues(new
                {
                    wishlist_name = wishlist.ListName,
                    owner = wishlist.Owner.Value,
                    slug = wishlist.Slug.Value,
                    visibility = wishlist.Visibility.ToString()
                });
        }

        [Fact]
        public async Task WishlistsRepository_GetByOwnerAsync_ShouldReturnWishlists()
        {
            Database.Setup.WithoutAnyWishlist();

            Database.Arrange.InsertMany(Tables.Wishlists, 10, id => new
            {
                wishlist_id = Guid.NewGuid(),
                owner = "George",
                slug = Slug.Of($"George's wish list {id}"),
                wishlist_name = $"George's wish list {id}",
                visibility = (id % 2 == 0) ? Visibility.Private.ToString() : Visibility.Public.ToString(),
                created = DateTime.UtcNow
            });

            var result = await Repository.GetByOwnerAsync(new Owner("George"), VisibilityCriteria.Public);

            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        // [Fact]
        // public async Task WishlistsRepository_ExistAsync_ShouldCheckIfWishlistExistsForTheOwner()
        // {
        //     var wishlist = FakeWishlist();
        //
        //     Database.Setup.WithoutAnyWishlist();
        //     Database.Arrange.WithOneWishlist(wishlist);
        //
        //     bool found = await Repository.ExistAsync(wishlist.Owner, wishlist.Slug);
        //     bool notFound = await Repository.ExistAsync(new Owner("Not found"), Slug.Empty);
        //
        //     found.Should().BeTrue();
        //     notFound.Should().BeFalse();
        // }

        // [Fact]
        // public async Task WishlistsRepository_ExistAsync_ShouldCheckIfWishlistExistsForTheId()
        // {
        //     var wishlist = FakeWishlist();
        //
        //     Database.Setup.WithoutAnyWishlist();
        //     Database.Arrange.WithOneWishlist(wishlist);
        //
        //     bool found = await Repository.ExistAsync(new Owner("George"), wishlist.Id);
        //     bool notFound = await Repository.ExistAsync(new Owner("George"), WishlistId.NewId());
        //
        //     found.Should().BeTrue();
        //     notFound.Should().BeFalse();
        // }

        [Fact]
        public async Task WishlistsRepository_DeleteAsync_ShouldDeleteWishlist()
        {
            var wishlist = FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            await Repository.DeleteAsync(wishlist.Id);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.Wishlists)
                .WithPrimaryKey(new
                {
                    wishlist_id = wishlist.Id.ToGuid()
                })
                .ShouldNotExists();
        }

        [Fact]
        public async Task WishlistsRepository_GetByIdAsync_ShouldReturnWishlist()
        {
            var newItem = FakeWishlistItem();
            var expectedWishlist = FakeWishlist(); //.With(ImmutableList.Create((WishlistItem)newItem));

            var wishlist = await Repository.GetByIdAsync(expectedWishlist.Id);

            wishlist.Should().NotBeNull();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldAddNewWishlistItem()
        {
            var wishlist = FakeWishlist();
            var newItem = FakeWishlistItem();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.Id.ToGuid()
                })
                .AndValues(new
                {
                    // catalog_item_id = newItem.CatalogItem.CatalogItemId.ToGuid(),
                    catalog_item_slug = newItem.CatalogItem.Slug
                })
                .ShouldExists();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldRemoveWishlistItem()
        {
            var newItem = FakeWishlistItem();
            var wishlist = FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.Id.ToGuid()
                })
                .ShouldNotExists();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldModifyWishlistItem()
        {
            var newItem = FakeWishlistItem();
            var wishlist = FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            var modified = newItem.With(
                priority: Priority.Low,
                price: Price.Euro(199M),
                notes: "Modified notes");

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    item_id = newItem.Id.ToGuid()
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

        private static Wishlist FakeWishlist() => throw new NotImplementedException();

        private static WishlistItem FakeWishlistItem() => throw new NotImplementedException();
    }
}