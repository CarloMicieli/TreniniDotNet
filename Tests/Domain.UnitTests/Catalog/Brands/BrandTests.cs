using System;
using System.Net.Mail;
using Xunit;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandTests
    {
        [Fact]
        public void Brand_ShouldCreateNewValues()
        {
            var b = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: "https://www.acmetreni.com",
                mailAddress: "mail@acmetreni.com",
                kind: BrandKind.Industrial);

            b.Should().NotBeNull();
            b.Name.Should().Be("ACME");
        }

        [Fact]
        public void Brand_ToBrandInfo_ShouldReturnTheBrandInfo()
        {
            var b = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME");

            var info = b.ToBrandInfo();

            info.Should().NotBeNull();
            info.BrandId.Should().Be(b.BrandId);
            info.Slug.Should().Be(b.Slug);
            info.Name.Should().Be(b.Name);
        }

        [Fact]
        public void Brand_Equals_ShouldCheckForBrandsEquality()
        {
            var acme1 = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: "https://www.acmetreni.com",
                mailAddress: "mail@acmetreni.com",
                kind: BrandKind.Industrial);
            var acme2 = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: "https://www.acmetreni.com",
                mailAddress: "mail@acmetreni.com",
                kind: BrandKind.Industrial);

            (acme1 == acme2).Should().BeTrue();
            acme1.Equals(acme2).Should().BeTrue();
        }

        [Fact]
        public void Brand_Equals_ShouldCheckForBrandsInequality()
        {
            var acme = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: "https://www.acmetreni.com",
                mailAddress: "mail@acmetreni.com",
                kind: BrandKind.Industrial);
            var roco = NewBrandWith(
                brandId: new BrandId(new Guid("ec168962-6191-474a-bec9-a07b74539307")),
                name: "Roco",
                companyName: "Roco Gmbh",
                websiteUrl: "https://www.roco.cc",
                kind: BrandKind.Industrial);

            (acme != roco).Should().BeTrue();
            acme.Equals(roco).Should().BeFalse();
            acme.Equals("it fails").Should().BeFalse();
        }

        [Fact]
        public void Brand_ToString_ShouldProduceStringRepresentation()
        {
            var b = NewBrandWith(name: "ACME");
            b.ToString().Should().Be("Brand(ACME)");
        }

        private static Brand NewBrandWith(
            BrandId? brandId = null,
            string name = null,
            string companyName = null,
            string websiteUrl = null,
            string mailAddress = null,
            BrandKind? kind = BrandKind.Industrial)
        {
            return new Brand(
                brandId ?? new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name ?? "ACME",
                Slug.Of(name ?? "ACME"),
                companyName ?? "Anonima Costruzione Modelli Esatti",
                null,
                null,
                websiteUrl == null ? new Uri("http://www.acmetreni.com") : new Uri(websiteUrl),
                mailAddress == null ? new MailAddress("mail@acmetreni.com") : new MailAddress(mailAddress),
                kind ?? BrandKind.Industrial,
                Address.With(
                    "address line 1", "address line 2",
                    "city name",
                    "region",
                    "postal code",
                    "IT"),
                Instant.FromUtc(1988, 11, 25, 9, 0),
                null,
                1);
        }
    }
}
