using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using Xunit;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class BrandInfoViewTests
    {
        [Fact]
        public void BrandInfoView_ShouldCreateViewFromValues()
        {
            var acme = CatalogSeedData.Brands.Acme();
            var view = new BrandInfoView(acme);

            view.Should().NotBeNull();
            view.Name.Should().Be(acme.Name);
            view.Slug.Should().Be(acme.Slug);
            view.Id.Should().Be(acme.BrandId.ToGuid());
        }
    }
}
