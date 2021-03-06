using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemCategoriesTests
    {
        [Fact]
        public void CatalogItemCategories_ShouldFindCategory_FromSingleRollingStockCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.NewAcme60392();

            var category = CatalogItemCategories.FromCatalogItem(item);

            category.Should().Be(CatalogItemCategory.Locomotives);
        }

        [Fact]
        public void CatalogItemCategories_ShouldFindCategory_FromMultipleRollingStockCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.NewRivarossiHR4298();

            var category = CatalogItemCategories.FromCatalogItem(item);

            category.Should().Be(CatalogItemCategory.PassengerCars);
        }

        [Fact]
        public void CatalogItemCategories_ShouldUseUnspecifiedCategory_FromCatalogItemWithoutRollingStocks()
        {
            var item = CatalogSeedData.CatalogItems.NewAcme999999();

            var category = CatalogItemCategories.FromCatalogItem(item);

            category.Should().Be(CatalogItemCategory.Unspecified);
        }
    }
}
