using System;
using FluentAssertions;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public class ShopInfoTests
    {
        [Fact]
        public void ShopInfo_TryCreate_ShouldCreateNewValues()
        {
            var id = Guid.NewGuid();

            var result = ShopInfo.TryCreate(id, "Shop name", "shop-name", out var info);

            result.Should().BeTrue();
            info.Should().NotBeNull();
            info.Id.Should().Be(new ShopId(id));
            info.Name.Should().Be("Shop name");
            info.Slug.Should().Be(Slug.Of("shop-name"));
        }

        [Fact]
        public void ShopInfo_TryCreate_ShouldFail_WhenAllValuesAreNull()
        {
            var id = Guid.NewGuid();

            var result = ShopInfo.TryCreate(null, null, null, out var info);

            result.Should().BeFalse();
            info.Should().BeNull();
        }
    }
}
