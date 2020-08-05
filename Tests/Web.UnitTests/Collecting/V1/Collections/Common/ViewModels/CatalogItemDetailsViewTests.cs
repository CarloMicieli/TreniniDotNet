using FluentAssertions;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.Collecting.V1.Common.ViewModels;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels
{
    public sealed class CatalogItemDetailsViewTests
    {
        [Fact]
        public void CatalogItemDetailsView_CreateFromCatalogItemDetails()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            var view = new CatalogItemDetailsView(catalogItem);

            view.ItemNumber.Should().Be("60392");
            view.Count.Should().Be(1);
            view.Description.Should().Be(catalogItem.Description);
            view.Category.Should().Be(CatalogItemCategory.Locomotives.ToString());
            view.Brand.Value.Should().Be("ACME");
            view.Brand.Slug.Should().Be("acme");
            view.Scale.Value.Should().Be("H0 (1:87)");
            view.Scale.Slug.Should().Be("h0");
        }
    }
}
