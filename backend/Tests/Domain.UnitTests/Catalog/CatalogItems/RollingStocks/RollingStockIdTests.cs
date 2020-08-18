using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public class RollingStockIdTests
    {
        [Fact]
        public void RollingStockId_ShouldConvertFromGuidAndBack()
        {
            var guid = Guid.NewGuid();

            var id = new RollingStockId(guid);
            Guid guid2 = id;
            guid2.Should().Be(guid);
        }

        [Fact]
        public void RollingStockId_ShouldCheckForEquality()
        {
            var guid = Guid.NewGuid();
            var id1 = new RollingStockId(guid);
            var id2 = new RollingStockId(guid);

            id1.Equals(id2).Should().BeTrue();
            (id1 == id2).Should().BeTrue();
            (id1 != id2).Should().BeFalse();
        }

        [Fact]
        public void RollingStockId_ShouldCheckForInequality()
        {
            var id1 = new RollingStockId(Guid.NewGuid());
            var id2 = new RollingStockId(Guid.NewGuid());

            id1.Equals(id2).Should().BeFalse();
            (id1 == id2).Should().BeFalse();
            (id1 != id2).Should().BeTrue();
        }
    }
}
