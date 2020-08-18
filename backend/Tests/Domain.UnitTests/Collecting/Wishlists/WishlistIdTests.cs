using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistIdTests
    {
        [Fact]
        public void WishlistId_ShouldCreateNewValues()
        {
            var id = WishlistId.NewId();
            id.Should().NotBeNull();
        }

        [Fact]
        public void WishlistId_ShouldConvertToGuidAndBack()
        {
            var cId = WishlistId.NewId();

            Guid id = cId;
            WishlistId cId2 = (WishlistId)id;

            cId.Should().Be(cId2);
        }

        [Fact]
        public void WishlistId_ShouldCheckForEquality()
        {
            var id1 = new WishlistId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            var id2 = new WishlistId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));

            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
            id1.Equals(id2).Should().BeTrue();
        }
    }
}
