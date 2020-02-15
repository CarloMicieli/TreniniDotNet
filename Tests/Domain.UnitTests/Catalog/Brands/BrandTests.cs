using TreniniDotNet.Common;
using System;
using System.Net.Mail;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandTests
    {
        [Fact]
        public void ItShouldCreateNewBrands()
        {
            var b = new Brand("ACME", null, new Uri("http://www.acmetreni.com"), new MailAddress("mail@acmetreni.com"), BrandKind.Industrial);
            Assert.Equal("Brand(ACME)", b.ToString());
        }

        [Fact]
        public void ItShouldCreateANewBrandIdWhenNotProvided()
        {
            var acme = new Brand("ACME", null, new Uri("http://www.acmetreni.com"), new MailAddress("mail@acmetreni.com"), BrandKind.Industrial);
            Assert.Equal(Slug.Of("acme"), acme.Slug);
        }

        [Fact]
        public void ItShouldReturnBrandPropertiesValues()
        {
            var name = "ACME";
            var companyName = "Anonima Costruzione Modelli Esatti";
            var website = new Uri("http://www.acmetreni.com");
            var emailAddress = new MailAddress("mail@acmetreni.com");
            var kind = BrandKind.Industrial;

            var b = new Brand(name, companyName, website, emailAddress, kind);
            Assert.Equal(name, b.Name);
            Assert.Equal(companyName, b.CompanyName);
            Assert.Equal(website, b.WebsiteUrl);
            Assert.Equal(emailAddress, b.EmailAddress);
            Assert.Equal(kind, b.Kind);
        }

        [Fact]
        public void ItShouldCheckForBrandsEquality()
        {
            var acme1 = Acme();
            var acme2 = Acme();

            Assert.True(acme1 == acme2);
            Assert.True(acme1.Equals(acme2));
        }

        [Fact]
        public void ItShouldCheckForBrandsInequality()
        {
            var acme = Acme();
            var roco = Roco();

            Assert.True(acme != roco);
            Assert.False(acme.Equals(roco));
            Assert.False(acme.Equals("it fails"));
        }

        [Fact]
        public void ItShouldThrowAnExceptionIfBrandNameIsBlank()
        {
            Assert.Throws<ArgumentException>(() => new Brand("", null, null, null, BrandKind.Industrial));
        }

        private static Brand Acme()
        {
            return new Brand(
                "ACME",
                "Anonima Costruzione Modelli Esatti",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial);
        }

        private static Brand Roco()
        {
            return new Brand(
                "Roco",
                null,
                new Uri("http://www.roco.cc"),
                new MailAddress("mail@roco.cc"),
                BrandKind.Industrial);
        }
    }
}
