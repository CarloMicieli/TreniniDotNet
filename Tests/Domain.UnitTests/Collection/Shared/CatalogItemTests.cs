using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public class CatalogItemTests
    {
        [Fact]
        public void CatalogItem_Of_ShouldCreateNewCatalogItems()
        {
            var itemNumber = new ItemNumber("123456");
            var item = CatalogItem.Of("ACME", itemNumber);

            item.Should().NotBeNull();
            item.Slug.Should().Be(Slug.Of("acme-123456"));
            item.ItemNumber.Should().Be(itemNumber);
            item.Brand.Should().Be("ACME");
        }

        [Fact]
        public void CatalogItem_New_ShouldCreateNewCatalogItems()
        {
            var itemSlug = Slug.Of("acme-123456");
            var itemNumber = new ItemNumber("123456");
            var item = new CatalogItem(itemSlug, "ACME", itemNumber);

            item.Should().NotBeNull();
            item.Slug.Should().Be(itemSlug);
            item.ItemNumber.Should().Be(itemNumber);
            item.Brand.Should().Be("ACME");
        }

        [Fact]
        public void CatalogItem_ShouldCheckEquality()
        {
            var item1 = CatalogItemWith(slug: Slug.Of("acme-123456"), "ACME", new ItemNumber("123456"));
            var item2 = CatalogItemWith(slug: Slug.Of("acme-123456"), "ACME", new ItemNumber("123456"));

            (item1 == item2).Should().BeTrue();
            item1.Equals(item2).Should().BeTrue();
        }

        [Fact]
        public void CatalogItem_ShouldCheckInequality()
        {
            var item1 = CatalogItemWith(slug: Slug.Of("acme-123456"), "ACME", new ItemNumber("123456"));
            var item2 = CatalogItemWith(slug: Slug.Of("acme-654321"), "ACME", new ItemNumber("654321"));

            (item1 != item2).Should().BeTrue();
            item1.Equals(item2).Should().BeFalse();
        }

        private static CatalogItem CatalogItemWith(
            Slug? slug = null,
            string brand = "ACME",
            ItemNumber? itemNumber = null)
        {
            return new CatalogItem(
                slug ?? Slug.Of("acme-123456"),
                brand,
                itemNumber ?? new ItemNumber("123456"));
        }
    }
}
