using System;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public class OwnerTests
    {
        [Fact]
        public void Owner_ShouldCreateValueWithNonemptyStrings()
        {
            var owner = new Owner("George");
            owner.Value.Should().Be("George");
        }

        [Fact]
        public void Owner_ShouldThrowAnExceptionForEmptyString()
        {
            Action act = () => new Owner("  ");
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Input value for owner is empty or null (Parameter 'value')");
        }

        [Fact]
        public void Owner_ShouldAutomaticallyConvertToString()
        {
            var owner = new Owner("George");

            string s = owner;
            s.Should().Be("George");
        }

        [Fact]
        public void Owner_ToString_ShouldProduceStringRepresentations()
        {
            var owner = new Owner("George");
            owner.ToString().Should().Be("Owner(George)");
        }

        [Fact]
        public void Owner_ShouldCheckEquality()
        {
            var george1 = new Owner("George");
            var george2 = new Owner("George");
            var richard = new Owner("richard");

            (george1 == george2).Should().BeTrue();
            (george1 != george2).Should().BeFalse();
            george1.Equals(george2).Should().BeTrue();
            george1.Equals(richard).Should().BeFalse();
            (george1 == richard).Should().BeFalse();
        }

        [Fact]
        public void Owner_CanViewWishlist_WhenIsTheOwner()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();
            var owner = wishlist.Owner;
            var richard = new Owner("richard");

            owner.CanView(wishlist).Should().BeTrue();
            richard.CanView(wishlist).Should().BeFalse();
        }

        [Fact]
        public void Owner_CanViewWishlist_WhenWishlistIsPublic()
        {
            var wishlist = CollectingSeedData.Wishlists.RocketFirstList();
            var owner = wishlist.Owner;
            var george = new Owner("George");

            owner.CanView(wishlist).Should().BeTrue();
            george.CanView(wishlist).Should().BeTrue();
        }

        [Fact]
        public void Owner_CanEditWishlist_OnlyWhenIsTheOwner()
        {
            var wishlist = CollectingSeedData.Wishlists.RocketFirstList();
            var owner = wishlist.Owner;
            var george = new Owner("George");

            owner.CanEdit(wishlist).Should().BeTrue();
            george.CanEdit(wishlist).Should().BeFalse();
        }
    }
}
