using TreniniDotNet.Common;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandInfoTests
    {
        [Fact]
        public void IsShouldExtract_BrandInfo_FromBrands()
        {
            var brand = CatalogSeedData.Brands.Roco();
            Assert.Equal(Slug.Of("roco"), brand.ToBrandInfo().Slug);
            Assert.Equal("Roco", brand.ToBrandInfo().Name);
        }

        [Fact]
        public void IsShouldReturnTheNameAsLabelFromBrandInfo()
        {
            var brand = CatalogSeedData.Brands.Roco();
            Assert.Equal("Roco", brand.ToBrandInfo().ToLabel());
        }
    }
}