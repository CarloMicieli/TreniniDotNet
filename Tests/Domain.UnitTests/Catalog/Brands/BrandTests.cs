using System;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandTests
    {
        [Fact]
        public void Brand_Equals_ShouldCheckForBrandsEquality()
        {
            var acme1 = CatalogSeedData.Brands.New()
                .Id(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"))
                .Name("ACME")
                .CompanyName("Associazione Costruzioni Modellistiche Esatte")
                .WebsiteUrl("http://www.acmetreni.com")
                .MailAddress("mail@acmetreni.com")
                .Industrial()
                .Build();

            var acme2 = CatalogSeedData.Brands.New()
                .Id(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"))
                .Name("ACME")
                .CompanyName("Associazione Costruzioni Modellistiche Esatte")
                .WebsiteUrl("http://www.acmetreni.com")
                .MailAddress("mail@acmetreni.com")
                .Industrial()
                .Build();

            (acme1 == acme2).Should().BeTrue();
            acme1.Equals(acme2).Should().BeTrue();
        }

        [Fact]
        public void Brand_Equals_ShouldCheckForBrandsInequality()
        {
            var acme = CatalogSeedData.Brands.New()
                .Id(new Guid("9ed9f089-2053-4a39-b669-a6d603080402"))
                .Name("ACME")
                .CompanyName("Associazione Costruzioni Modellistiche Esatte")
                .WebsiteUrl("http://www.acmetreni.com")
                .MailAddress("mail@acmetreni.com")
                .Industrial()
                .Build();
            var roco = CatalogSeedData.Brands.New()
                .Id(new Guid("4b7a619b-65cc-41f5-a003-450537c85dea"))
                .Name("Roco")
                .CompanyName("Modelleisenbahn GmbH")
                .WebsiteUrl("http://www.roco.cc")
                .MailAddress("webshop@roco.cc")
                .Industrial()
                .Build();

            (acme != roco).Should().BeTrue();
            acme.Equals(roco).Should().BeFalse();
            acme.Equals("it fails").Should().BeFalse();
        }

        [Fact]
        public void Brand_ToString_ShouldProduceStringRepresentation()
        {
            var b = CatalogSeedData.Brands.New().Name("ACME").Build();
            b.ToString().Should().Be("Brand(ACME)");
        }

        [Fact]
        public void Brand_With_ShouldProduceAModifiedNewValue()
        {
            var modifiedAcme = CatalogSeedData.Brands.NewAcme()
                .With(groupName: "Modified ACME Group");
            modifiedAcme.Should().NotBeNull();
            modifiedAcme.GroupName.Should().Be("Modified ACME Group");
        }
    }
}
