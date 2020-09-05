using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistsFactoryTests
    {
        private WishlistsFactory Factory { get; }

        public WishlistsFactoryTests()
        {
            Factory = new WishlistsFactory(
                FakeClock.FromUtc(1988, 11, 25),
                FakeGuidSource.NewSource(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
        }

        [Fact]
        public void WishlistsFactory_ShouldCreateNewValues()
        {
            var wishlist = Factory.CreateWishlist(
                new Owner("George"),
                "My first list",
                Visibility.Private,
                new Budget(1000M, "EUR"));

            wishlist.Should().NotBeNull();
            wishlist.Id.Should().Be(new WishlistId(new Guid("af3f666c-8f81-438f-bff6-0d32f13b7eef")));
            wishlist.Owner.Should().Be(new Owner("George"));
            wishlist.Slug.Should().Be(Slug.Of("george-my-first-list"));
            wishlist.ListName.Should().Be("My first list");
            wishlist.Visibility.Should().Be(Visibility.Private);
            wishlist.Budget.Should().Be(new Budget(1000M, "EUR"));
            wishlist.Items.Should().HaveCount(0);
            wishlist.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0, 0));
            wishlist.ModifiedDate.Should().BeNull();
            wishlist.Version.Should().Be(1);
        }
    }
}
