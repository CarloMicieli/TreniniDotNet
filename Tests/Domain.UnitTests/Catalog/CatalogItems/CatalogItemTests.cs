using Xunit;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemTests
    {
        [Fact]
        public void CatalogItem_ShouldCheckForEquality()
        {
            var item1 = CatalogSeedData.CatalogItems.Acme_60392();
            var item2 = CatalogSeedData.CatalogItems.Rivarossi_HR4298();

            (item1.Equals(item1)).Should().BeTrue();
            (item1.Equals(item2)).Should().BeFalse();
        }

        [Fact]
        public void CatalogItem_With_ShouldProduceNewModifiedValues()
        {
            var modified = CatalogSeedData.CatalogItems.Acme_60458()
                .With(prototypeDescription: "modified prototype desc");

            modified.Should().NotBeNull();
            modified.PrototypeDescription.Should().Be("modified prototype desc");
        }
    }
}
