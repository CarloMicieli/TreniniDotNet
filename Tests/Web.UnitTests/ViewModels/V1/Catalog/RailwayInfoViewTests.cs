using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class RailwayInfoViewTests
    {
        [Fact]
        public void RailwayInfoView_ShouldReturnViewFromValues()
        {
            var fs = CatalogSeedData.Railways.Fs();

            var view = new RailwayInfoView(fs);

            view.Should().NotBeNull();
            view.Slug.Should().Be(fs.Slug);
            view.Name.Should().Be(fs.Name);
            view.Id.Should().Be(fs.RailwayId.ToGuid());
        }
    }
}
