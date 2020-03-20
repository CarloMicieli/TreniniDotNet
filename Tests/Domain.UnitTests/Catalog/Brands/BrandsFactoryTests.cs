using Xunit;
using FluentAssertions;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using System.Net.Mail;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandsFactoryTests
    {
        private readonly IBrandsFactory factory;

        public BrandsFactoryTests()
        {
            this.factory = new BrandsFactory();
        }

        [Fact]
        public void BrandsFactory_ShouldCreateBrands_WithValidation()
        {
            Guid id = Guid.NewGuid();
            var success = factory.NewBrandV(
                id,
                "name",
                "slug",
                "company name",
                "https://www.website.com",
                "mail@mail.com",
                "industrial"
            );

            success.Match(
                Succ: succ =>
                {
                    succ.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
                    succ.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
                    succ.Kind.Should().Be(BrandKind.Industrial);
                },
                Fail: errors => Assert.True(false, "should never get here"));
        }

        [Fact]
        public void BrandsFactory_ShouldReturnsFailure_WhenFailedValidation()
        {
            Guid id = Guid.NewGuid();
            var failure = factory.NewBrandV(
                id,
                "name",
                "slug",
                null,
                "---invalid url---",
                "---invalid mail---",
                "---invalid kind---"
            );

            failure.Match(
                Succ: succ => Assert.True(false, "should never get here"),
                Fail: errors =>
                {
                    errors.Should().HaveCount(3);

                    var errorsList = errors.ToList();

                    errorsList.Should().ContainInOrder(
                        Error.New("Invalid URI: The format of the URI could not be determined."),
                        Error.New("The specified string is not in the form required for an e-mail address."),
                        Error.New("The specified string is not a valid brand kind."));

                });
        }

        [Fact]
        public void BrandsFactory_Should_CreateNewBrandsFromPrimitives()
        {
            Guid id = Guid.NewGuid();
            IBrand brand = factory.NewBrand(
                id,
                "name",
                "slug",
                "company name",
                "https://www.website.com",
                "mail@mail.com",
                "industrial"
            );

            brand.BrandId.Should().Be(new BrandId(id));
            brand.Name.Should().Be("name");
            brand.Slug.Should().Be(Slug.Of("slug"));
            brand.CompanyName.Should().Be("company name");
            brand.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
            brand.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
            brand.Kind.Should().Be(BrandKind.Industrial);
        }

        [Fact]
        public void BrandsFactory_ShouldCreateNewBrand_WithIndustialAsDefaultKind()
        {
            Guid id = Guid.NewGuid();
            IBrand brand = factory.NewBrand(
                id,
                "name",
                "slug",
                "company name",
                "https://www.website.com",
                "mail@mail.com",
                null
            );

            brand.Kind.Should().Be(BrandKind.Industrial);
        }

        [Fact]
        public void BrandsFactory_Should_CreateNewBrandsFromDomainObjects()
        {
            BrandId id = new BrandId(Guid.NewGuid());
            IBrand brand = factory.NewBrand(
                id,
                "name",
                Slug.Of("slug"),
                "company name",
                new Uri("https://www.website.com"),
                new MailAddress("mail@mail.com"),
                BrandKind.Industrial
            );

            brand.BrandId.Should().Be(id);
            brand.Name.Should().Be("name");
            brand.Slug.Should().Be(Slug.Of("slug"));
            brand.CompanyName.Should().Be("company name");
            brand.WebsiteUrl.Should().Be(new Uri("https://www.website.com"));
            brand.EmailAddress.Should().Be(new MailAddress("mail@mail.com"));
            brand.Kind.Should().Be(BrandKind.Industrial);
        }

        [Fact]
        public void BrandsFactory_ShouldLeaveMailAddressNull_WhenProvidedValueIsBlank()
        {
            IBrand brand = factory.NewBrand(
                Guid.NewGuid(),
                "name",
                "slug",
                "company name",
                "https://www.website.com",
                "",
                "industrial"
            );

            brand.EmailAddress.Should().BeNull();
        }

        [Fact]
        public void BrandsFactory_ShouldLeaveWebSiteNull_WhenProvidedValueIsBlank()
        {
            IBrand brand = factory.NewBrand(
                Guid.NewGuid(),
                "name",
                "slug",
                "company name",
                "",
                "mail@mail.com",
                "industrial"
            );

            brand.WebsiteUrl.Should().BeNull();
        }

        [Fact]
        public void BrandsFactory_ShouldThrowArgumentException_WhenProvidedMailAddressIsInvalid()
        {
            Action act = () => factory.NewBrand(
                Guid.NewGuid(),
                "name",
                "slug",
                "company name",
                null,
                "invalid email",
                "industrial"
            );

            act.Should().Throw<FormatException>()
                .WithMessage("The specified string is not in the form required for an e-mail address.");
        }

        [Fact]
        public void BrandsFactory_ShouldThrowArgumentException_WhenProvidedWebsiteUrlIsInvalid()
        {
            Action act = () => factory.NewBrand(
                Guid.NewGuid(),
                "name",
                "slug",
                "company name",
                "invalid uri",
                null,
                "industrial"
            );

            act.Should().Throw<UriFormatException>()
                .WithMessage("Invalid URI: The format of the URI could not be determined.");
        }
    }
}