using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public class CollectionCategoriesTests
    {
        [Fact]
        public void CollectionCategories_ShouldFindCategory_FromSingleRollingStockCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            var category = CollectionCategories.FromCatalogItem(item);

            category.Should().Be(CollectionCategory.Locomotives);
        }

        [Fact]
        public void CollectionCategories_ShouldFindCategory_FromMultipleRollingStockCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Rivarossi_HR4298();

            var category = CollectionCategories.FromCatalogItem(item);

            category.Should().Be(CollectionCategory.PassengerCars);
        }

        [Fact]
        public void CollectionCategories_ShouldUseUnspecifiedCategory_FromCatalogItemWithoutRollingStocks()
        {
            var item = CatalogSeedData.CatalogItems.Acme_999999();

            var category = CollectionCategories.FromCatalogItem(item);

            category.Should().Be(CollectionCategory.Unspecified);
        }
    }
}
