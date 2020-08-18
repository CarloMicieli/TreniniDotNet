using System;
using System.Net.Mail;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandsFactoryTests
    {
        private static Instant _expectedDate = Instant.FromUtc(1988, 11, 25, 0, 0);
        private static BrandId _expectedBrandId = new BrandId(new Guid("fab083e5-7c33-4276-9068-e2de39c30281"));

        private BrandsFactory Factory { get; }

        public BrandsFactoryTests()
        {
            var guidSource = FakeGuidSource.NewSource(_expectedBrandId);
            Factory = new BrandsFactory(new FakeClock(_expectedDate), guidSource);
        }

        [Fact]
        public void BrandsFactory_ShouldCreateNewBrands()
        {
            var expectedAddress = Address.With(
                line1: "22 Acacia Avenue",
                city: "London",
                postalCode: "123456",
                country: "UK");

            var b = Factory.CreateBrand(
                "NAME",
                "company name",
                "group name",
                "description",
                new Uri("https://www.website.com"),
                new MailAddress("mail@mail.com"),
                BrandKind.Industrial,
                expectedAddress);
            b.Should().NotBeNull();
            b.Id.Should().Be(_expectedBrandId);
            b.Name.Should().Be("NAME");
            b.Slug.Should().Be(Slug.Of("name"));
            b.CompanyName.Should().Be("company name");
            b.Description.Should().Be("description");
            b.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
            b.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
            b.Address.Should().Be(expectedAddress);
            b.Kind.Should().Be(BrandKind.Industrial);
            b.Version.Should().Be(1);
            b.CreatedDate.Should().Be(_expectedDate);
        }
    }
}
