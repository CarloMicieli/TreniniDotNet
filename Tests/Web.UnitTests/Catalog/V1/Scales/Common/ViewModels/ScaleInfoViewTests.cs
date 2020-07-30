using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels
{
    public class ScaleInfoViewTests
    {
        [Fact]
        public void ScaleInfoView_ShouldReturnViewFromValues()
        {
            var H0 = CatalogSeedData.Scales.ScaleH0();

            var view = new ScaleInfoView(H0);

            view.Should().NotBeNull();
            view.Slug.Should().Be(H0.Slug);
            view.Name.Should().Be(H0.Name);
            view.Id.Should().Be(H0.Id.ToGuid());
        }
    }
}
