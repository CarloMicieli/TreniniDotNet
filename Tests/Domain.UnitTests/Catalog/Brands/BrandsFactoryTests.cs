using Xunit;
using FluentAssertions;
using System;
using System.Net.Mail;
using NodaTime.Testing;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandsFactoryTests
    {
        private static Instant ExpectedDate = Instant.FromUtc(1988, 11, 25, 0, 0);
        private static BrandId ExpectedBrandId = new BrandId(new Guid("fab083e5-7c33-4276-9068-e2de39c30281"));

        private readonly IGuidSource guidSource;
        private readonly IBrandsFactory factory;

        public BrandsFactoryTests()
        {
            guidSource = FakeGuidSource.NewSource(ExpectedBrandId.ToGuid());
            factory = new BrandsFactory(new FakeClock(ExpectedDate), guidSource);
        }

        [Fact]
        public void BrandsFactory_UpdateBrand_ShouldBumpVersionAndSetModifiedDate()
        {
            var acme = CatalogSeedData.Brands.Acme();

            var updated = factory.UpdateBrand(acme, null, null, null, null, null, null, null, null);

            updated.Version.Should().Be(2);
            updated.ModifiedDate.Should().Be(ExpectedDate);
        }

        [Fact]
        public void BrandsFactory_CreateNewBrand_ShouldCreateBrandsFromDomainObjects()
        {
            Address ExpectedAddress = Address.With(
                line1: "22 Acacia Avenue",
                city: "London",
                postalCode: "123456",
                country: "UK");

            IBrand b = factory.CreateNewBrand(
                "NAME",
                "company name",
                "group name",
                "description",
                new Uri("https://www.website.com"),
                new MailAddress("mail@mail.com"),
                BrandKind.Industrial,
                ExpectedAddress);

            b.Should().NotBeNull();
            b.BrandId.Should().Be(ExpectedBrandId);
            b.Name.Should().Be("NAME");
            b.Slug.Should().Be(Slug.Of("name"));
            b.CompanyName.Should().Be("company name");
            b.Description.Should().Be("description");
            b.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
            b.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
            b.Address.Should().Be(ExpectedAddress);
            b.Kind.Should().Be(BrandKind.Industrial);
            b.Version.Should().Be(1);
            b.CreatedDate.Should().Be(ExpectedDate);
        }

        [Fact]
        public void BrandsFactory_NewBrand_ShouldCreateBrands()
        {
            Address ExpectedAddress = Address.With(
                    line1: "22 Acacia Avenue",
                    city: "London",
                    postalCode: "123456",
                    country: "UK");

            IBrand b = factory.NewBrand(
                ExpectedBrandId.ToGuid(),
                "NAME",
                "name",
                BrandKind.Industrial.ToString(),
                "company name",
                "group name",
                "description",
                "https://www.website.com",
                "mail@mail.com",
                ExpectedAddress,
                ExpectedDate.ToDateTimeUtc(),
                null,
                2);

            b.Should().NotBeNull();
            b.BrandId.Should().Be(ExpectedBrandId);
            b.Name.Should().Be("NAME");
            b.Slug.Should().Be(Slug.Of("name"));
            b.CompanyName.Should().Be("company name");
            b.Description.Should().Be("description");
            b.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
            b.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
            b.Kind.Should().Be(BrandKind.Industrial);
            b.Address.Should().Be(ExpectedAddress);
            b.Version.Should().Be(2);
            b.CreatedDate.Should().Be(ExpectedDate);
        }

        [Fact]
        public void BrandsFactory_NewBrand_ShouldSetNullTheInvalidValues()
        {
            Address ExpectedAddress = Address.With(line1: "", city: "", postalCode: "");

            IBrand b = factory.NewBrand(
                ExpectedBrandId.ToGuid(),
                "NAME",
                "name",
                BrandKind.Industrial.ToString(),
                "company name",
                "group name",
                "description",
                "--invalid url--",
                "--invalid email--",
                Address.With(),
                ExpectedDate.ToDateTimeUtc(),
                null,
                2);

            b.Should().NotBeNull();
            b.WebsiteUrl.Should().BeNull();
            b.EmailAddress.Should().BeNull();
            b.Address.Should().Be(ExpectedAddress);
        }
    }
}