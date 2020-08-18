using FluentAssertions;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels
{
    public class BrandInfoViewTests
    {
        [Fact]
        public void BrandInfoView_ShouldCreateViewFromValues()
        {
            var acme = new BrandRef(CatalogSeedData.Brands.NewAcme());

            var view = new BrandInfoView(acme);

            view.Should().NotBeNull();
            view.Name.Should().Be(acme.ToString()); //TODO
            view.Slug.Should().Be(acme.Slug);
            view.Id.Should().Be(acme.Id);
        }
    }
}
