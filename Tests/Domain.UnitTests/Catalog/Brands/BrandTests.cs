using System;
using System.Net.Mail;
using FluentAssertions;
using Xunit;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using static TreniniDotNet.TestHelpers.SeedData.Catalog.CatalogSeedData;

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
                websiteUrl: new Uri("https://www.acmetreni.com"),
                mailAddress: new MailAddress("mail@acmetreni.com"),
                brandKind: BrandKind.Industrial);

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
                websiteUrl: new Uri("https://www.acmetreni.com"),
                mailAddress: new MailAddress("mail@acmetreni.com"),
                brandKind: BrandKind.Industrial);
            var acme2 = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: new Uri("https://www.acmetreni.com"),
                mailAddress: new MailAddress("mail@acmetreni.com"),
                brandKind: BrandKind.Industrial);

            acme1.Equals(acme2).Should().BeTrue();
        }

        [Fact]
        public void Brand_Equals_ShouldCheckForBrandsInequality()
        {
            var acme = NewBrandWith(
                brandId: new BrandId(new Guid("5685961f-b0ca-4c66-ae22-df2fabe32666")),
                name: "ACME",
                companyName: "Anonima Costruzione Modelli Esatti",
                websiteUrl: new Uri("https://www.acmetreni.com"),
                mailAddress: new MailAddress("mail@acmetreni.com"),
                brandKind: BrandKind.Industrial);
            var roco = NewBrandWith(
                brandId: new BrandId(new Guid("ec168962-6191-474a-bec9-a07b74539307")),
                name: "Roco",
                companyName: "Roco Gmbh",
                websiteUrl: new Uri("https://www.roco.cc"),
                brandKind: BrandKind.Industrial);

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

        [Fact]
        public void Brand_With_ShouldProduceAModifiedNewValue()
        {
            var modifiedAcme = CatalogSeedData.Brands.Acme().With(groupName: "Modified ACME Group");
            modifiedAcme.Should().NotBeNull();
            modifiedAcme.GroupName.Should().Be("Modified ACME Group");
        }
    }
}
