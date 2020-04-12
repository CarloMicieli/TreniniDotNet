using Xunit;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class CatalogItemViewTests
    {
        [Fact]
        public void CatalogItemView_Should_RenderCatalogItemViews()
        {
            var catItem = CatalogSeedData.CatalogItems.Acme_60392();
            var view = new CatalogItemView(catItem, new LinksView());

            view.Should().NotBeNull();

            view.Id.Should().Be(catItem.CatalogItemId.ToGuid());
            view.ItemNumber.Should().Be(catItem.ItemNumber.ToString());

            view.Brand.Should().NotBeNull();
            view.Brand.Name.Should().Be(catItem.Brand.Name);

            view.Scale.Should().NotBeNull();
            view.Scale.Name.Should().Be(catItem.Scale.Name);

            view.RollingStocks.Should().NotBeNull();
            view.RollingStocks.Should().HaveCount(1);
        }
    }
}
