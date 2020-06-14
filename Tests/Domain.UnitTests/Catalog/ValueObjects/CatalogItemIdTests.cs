using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public class CatalogItemIdTests
    {
        [Fact]
        public void CatalogItemId_ShouldCheckEquality()
        {
            var guid = Guid.NewGuid();

            var id1 = new CatalogItemId(guid);
            var id2 = new CatalogItemId(guid);

            (id1 == id2).Should().BeTrue();
        }
    }
}
