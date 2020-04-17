using Xunit;
using FluentAssertions;
using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public class CatalogRefTests
    {
        [Fact]
        public void CatalogRef_Of_ShouldCreateNewCatalogRefs()
        {
            var id = Guid.NewGuid();
            var item = CatalogRef.Of(id, "acme-123456");

            item.Should().NotBeNull();
            item.CatalogItemId.Should().Be(new CatalogItemId(id));
            item.Slug.Should().Be(Slug.Of("acme-123456"));
        }

        [Fact]
        public void CatalogRef_ShouldCheckEquality()
        {
            var id = Guid.NewGuid();
            var item1 = CatalogRef.Of(id, "acme-123456");
            var item2 = CatalogRef.Of(id, "acme-123456");

            item1.Equals(item2).Should().BeTrue();
            // (item1 == item2).Should().BeTrue();
        }

        [Fact]
        public void CatalogRef_ShouldCheckInequality()
        {
            var item1 = CatalogRef.Of(Guid.NewGuid(), "acme-123456");
            var item2 = CatalogRef.Of(Guid.NewGuid(), "acme-654321");

            (item1 != item2).Should().BeTrue();
            item1.Equals(item2).Should().BeFalse();
        }
    }
}
