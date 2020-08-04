using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public class WishlistsRepositoryTests : DapperRepositoryUnitTests<IWishlistsRepository>
    {
        public WishlistsRepositoryTests()
            : base(unitOfWork => new WishlistsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task WishlistsRepository_AddAsync_ShouldCreateWishlist()
        {
            Database.ArrangeWithoutAnyWishlist();
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

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
            Database.ArrangeWithoutAnyWishlist();

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

        [Fact]
        public async Task WishlistsRepository_CountWishlistsAsync_ShouldCountOwnerNumberOfWishlists()
        {
            Database.ArrangeWithoutAnyWishlist();

            Database.Arrange.InsertMany(Tables.Wishlists, 10, id => new
            {
                wishlist_id = Guid.NewGuid(),
                owner = "George",
                slug = Slug.Of($"George's wish list {id}"),
                wishlist_name = $"George's wish list {id}",
                visibility = (id % 2 == 0) ? Visibility.Private.ToString() : Visibility.Public.ToString(),
                created = DateTime.UtcNow
            });

            var result1 = await Repository.CountWishlistsAsync(new Owner("George"));
            var result2 = await Repository.CountWishlistsAsync(new Owner("Not found"));

            result1.Should().Be(10);
            result2.Should().Be(0);
        }

        [Fact]
        public async Task WishlistsRepository_ExistsAsync_ShouldCheckIfWishlistExistsForTheOwner()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(wishlist);

            var found = await Repository.ExistsAsync(wishlist.Owner, wishlist.ListName!);
            var notFound = await Repository.ExistsAsync(new Owner("Not found"), "not found");

            found.Should().BeTrue();
            notFound.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsRepository_ExistsAsync_ShouldCheckIfWishlistExistsForTheId()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(wishlist);

            var found = await Repository.ExistsAsync(wishlist.Id);
            var notFound = await Repository.ExistsAsync(WishlistId.NewId());

            found.Should().BeTrue();
            notFound.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsRepository_DeleteAsync_ShouldDeleteWishlist()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(wishlist);

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
            var expectedWishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(expectedWishlist);

            var wishlist = await Repository.GetByIdAsync(expectedWishlist.Id);

            wishlist.Should().NotBeNull();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldAddNewWishlistItem()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(wishlist);

            var modified = wishlist.Items.First()
                .With(notes: "Modified notes");
            wishlist.UpdateItem(modified);

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    wishlist_item_id = modified.Id.ToGuid()
                })
                .AndValues(new
                {
                    notes = "Modified notes"
                })
                .ShouldExists();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldRemoveWishlistItem()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var wishlistItemId = wishlist.Items.First().Id;
            wishlist.RemoveItem(wishlistItemId, new LocalDate(2020, 11, 25));

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    wishlist_item_id = wishlistItemId.ToGuid()
                })
                .ShouldNotExists();
        }

        [Fact]
        public async Task WishlistsRepository_UpdateAsync_ShouldModifyWishlistItem()
        {
            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            Database.ArrangeWithOneWishlist(wishlist);

            var modified = wishlist.Items.First()
                .With
                (
                    priority: Priority.Low,
                    price: Price.Euro(199M),
                    notes: "Modified notes"
                );
            wishlist.UpdateItem(modified);

            await Repository.UpdateAsync(wishlist);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.WishlistItems)
                .WithPrimaryKey(new
                {
                    wishlist_item_id = modified.Id.ToGuid()
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
    }
}