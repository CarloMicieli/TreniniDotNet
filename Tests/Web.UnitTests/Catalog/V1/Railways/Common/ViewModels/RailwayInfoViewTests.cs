using FluentAssertions;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels
{
    public class RailwayInfoViewTests
    {
        [Fact]
        public void RailwayInfoView_ShouldReturnViewFromValues()
        {
            var fs = new RailwayRef(CatalogSeedData.Railways.NewFs());

            var view = new RailwayInfoView(fs);

            view.Should().NotBeNull();
            view.Slug.Should().Be(fs.Slug);
            view.Name.Should().Be(fs.ToString()); //TODO
            view.Id.Should().Be(fs.Id);
        }
    }
}
