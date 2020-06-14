using FluentAssertions;
using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandInfoTests
    {
        [Fact]
        public void BrandInfo_ShouldExtract_FromBrands()
        {
            var brand = CatalogSeedData.Brands.Roco();

            brand.ToBrandInfo().Slug.Should().Be(Slug.Of("roco"));
            brand.ToBrandInfo().Name.Should().Be("Roco");
        }

        [Fact]
        public void BrandInfo_ShouldReturnTheNameAsLabelFromBrandInfo()
        {
            var brand = CatalogSeedData.Brands.Roco();
            brand.ToBrandInfo().ToLabel().Should().Be("Roco");
        }
    }
}