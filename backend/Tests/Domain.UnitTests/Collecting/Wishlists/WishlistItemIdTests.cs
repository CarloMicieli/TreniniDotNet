using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistItemIdTests
    {
        [Fact]
        public void WishlistItemId_ShouldCreateNewValues()
        {
            var id = WishlistItemId.NewId();
            id.Should().NotBeNull();
        }

        [Fact]
        public void WishlistItemId_ShouldConvertToGuidAndBack()
        {
            var cId = WishlistItemId.NewId();

            Guid id = cId;
            WishlistItemId cId2 = (WishlistItemId)id;

            cId.Should().Be(cId2);
        }

        [Fact]
        public void WishlistItemId_ShouldCheckForEquality()
        {
            var id1 = new WishlistItemId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            var id2 = new WishlistItemId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));

            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
            id1.Equals(id2).Should().BeTrue();
        }
    }
}
