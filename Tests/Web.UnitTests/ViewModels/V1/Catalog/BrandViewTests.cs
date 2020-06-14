using System;
using System.Net.Mail;
using FluentAssertions;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using Xunit;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class BrandViewTests
    {
        [Fact]
        public void BrandView_ShouldRenderBrands_WithBasicInfo()
        {
            Guid expectedId = new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313");
            var view = BrandViewWith(id: expectedId);

            view.Should().NotBeNull();
            view.Id.Should().Be(expectedId);
            view.Name.Should().Be("Rivarossi");
            view.CompanyName.Should().Be("Rivarossi Como");
            view.MailAddress.Should().Be("mail@rivarossi.com");
            view.Kind.Should().Be("Industrial");
            view.WebsiteUrl.Should().Be("http://www.rivarossi.com/");
        }

        [Fact]
        public void BrandView_ShouldRenderBrands_WithAddress()
        {
            var expectedAddress = Address.With(
                line1: "via Conciliazione n. 74",
                postalCode: "22100",
                city: "Como",
                country: "IT");
            var view = BrandViewWith(address: expectedAddress);

            view.Should().NotBeNull();
            view.Address.Should().NotBeNull();
            view.Address.Line1.Should().Be(expectedAddress.Line1);
            view.Address.Line2.Should().Be(expectedAddress.Line2);
            view.Address.Region.Should().Be(expectedAddress.Region);
            view.Address.City.Should().Be(expectedAddress.City);
            view.Address.PostalCode.Should().Be(expectedAddress.PostalCode);
            view.Address.Country.Should().Be(expectedAddress.Country);
        }

        public static BrandView BrandViewWith(Guid? id = null, Address address = null)
        {
            IBrand brand = CatalogSeedData.NewBrandWith(
                new BrandId(id ?? new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")),
                "Rivarossi",
                "Rivarossi Como",
                websiteUrl: new Uri("http://www.rivarossi.com"),
                mailAddress: new MailAddress("mail@rivarossi.com"),
                address: address);

            return new BrandView(brand, new LinksView());
        }
    }
}
