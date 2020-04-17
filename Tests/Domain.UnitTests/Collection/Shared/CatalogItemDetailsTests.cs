using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public class CatalogItemDetailsTests
    {
        [Fact]
        public void CatalogItemDetails_ShouldCreateNewValue_FromCatalogItems()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var details = CatalogItemDetails.FromCatalogItem(item);

            details.Should().NotBeNull();
            details.Brand.Name.Should().Be("ACME");
            details.Scale.Name.Should().Be("H0 (1:87)");
            details.ItemNumber.Should().Be(new ItemNumber("60392"));
            details.RollingStocksCount.Should().Be(1);
            details.Category.Should().Be(CollectionCategory.Locomotives);
        }
    }
}
