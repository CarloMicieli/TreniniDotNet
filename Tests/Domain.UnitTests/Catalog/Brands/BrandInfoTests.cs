using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandInfoTests
    {
        [Fact]
        public void IsShouldExtract_BrandInfo_FromBrands()
        {
            var brand = Roco();
            Assert.Equal(Slug.Of("roco"), brand.ToBrandInfo().Slug);
            Assert.Equal("Roco", brand.ToBrandInfo().Name);
        }

        [Fact]
        public void IsShouldReturnTheNameAsLabelFromBrandInfo()
        {
            var brand = Roco();
            Assert.Equal("Roco", brand.ToBrandInfo().ToLabel());
        }

        private static IBrand Roco()
        {
            return new Brand(
                BrandId.NewId(),
                "Roco",
                Slug.Of("roco"),
                null,
                new Uri("http://www.roco.cc"),
                new MailAddress("mail@roco.cc"),
                BrandKind.Industrial);
        }
    }
}