using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public class ShopIdTests
    {
        [Fact]
        public void ShopId_ShouldCreateNewValues()
        {
            var id = ShopId.NewId();
            id.Should().NotBeNull();
        }

        [Fact]
        public void ShopId_ShouldConvertToGuidAndBack()
        {
            var shopId = ShopId.NewId();

            Guid id = shopId;
            ShopId shopId2 = (ShopId)id;

            shopId.Should().Be(shopId2);
        }

        [Fact]
        public void ShopId_ShouldCheckForEquality()
        {
            var id1 = new ShopId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            var id2 = new ShopId(new Guid("1613c84d-48d6-4478-a830-cc0aa06c504a"));
            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
            id1.Equals(id2).Should().BeTrue();
        }
    }
}
