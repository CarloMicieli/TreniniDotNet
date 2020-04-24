using FluentAssertions;
using NodaTime;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public class WishlistsRepositoryTests : CollectionRepositoryUnitTests<IWishlistsRepository>
    {
        public WishlistsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IWishlistsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new WishlistsRepository(databaseContext, new WishlistsFactory(clock, new GuidSource()));

        [Fact]
        public async Task WishlistsRepository_AddAsync_ShouldCreateWishlist()
        {
            Database.Setup.WithoutAnyWishlist();

            var wishlist = new FakeWishlist();

            var id = await Repository.AddAsync(wishlist);

            id.Should().Be(wishlist.WishlistId);

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

            Database.Arrange.InsertMany(Tables.Wishlists, 10, id =>
            {
                return new
                {
                    wishlist_id = Guid.NewGuid(),
                    owner = "George",
                    slug = Slug.Of($"George's wish list {id}"),
                    wishlist_name = $"George's wish list {id}",
                    visibility = (id % 2 == 0) ? Visibility.Private.ToString() : Visibility.Public.ToString(),
                    created = DateTime.UtcNow
                };
            });

            var result = await Repository.GetByOwnerAsync(new Owner("George"), VisibilityCriteria.Public);

            result.Should().NotBeNull();
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task WishlistsRepository_ExistAsync_ShouldCheckIfWishlistExistsForTheOwner()
        {
            var wishlist = new FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            bool found = await Repository.ExistAsync(wishlist.Owner, wishlist.Slug);
            bool notFound = await Repository.ExistAsync(new Owner("Not found"), Slug.Empty);

            found.Should().BeTrue();
            notFound.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsRepository_ExistAsync_ShouldCheckIfWishlistExistsForTheId()
        {
            var wishlist = new FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            bool found = await Repository.ExistAsync(new Owner("George"), wishlist.WishlistId);
            bool notFound = await Repository.ExistAsync(new Owner("George"), WishlistId.NewId());

            found.Should().BeTrue();
            notFound.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsRepository_DeleteAsync_ShouldDeleteWishlist()
        {
            var wishlist = new FakeWishlist();

            Database.Setup.WithoutAnyWishlist();
            Database.Arrange.WithOneWishlist(wishlist);

            await Repository.DeleteAsync(wishlist.WishlistId);

            Database.Assert.RowInTable(Tables.Wishlists)
                .WithPrimaryKey(new
                {
                    wishlist_id = wishlist.WishlistId.ToGuid()
                })
                .ShouldNotExists();
        }

        [Fact]
        public async Task WishlistsRepository_GetByIdAsync_ShouldReturnWishlist()
        {
            var newItem = new FakeWishlistItem();
            var expectedWishlist = new FakeWishlist().With(
                Items: ImmutableList.Create((IWishlistItem)newItem));

            var wishlist = await Repository.GetByIdAsync(expectedWishlist.WishlistId);

            wishlist.Should().NotBeNull();
        }
    }
}
