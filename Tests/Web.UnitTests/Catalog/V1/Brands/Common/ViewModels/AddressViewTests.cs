using FluentAssertions;
using TreniniDotNet.SharedKernel.Addresses;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.ViewModels
{
    public class AddressViewTests
    {
        [Fact]
        public void AddressView_ShouldRenderAddresses()
        {
            var a = Address.With(
                line1: "22 Acacia Avenue",
                line2: "West End",
                city: "London",
                postalCode: "12345",
                country: "UK");


            var view = new AddressView(a!);

            view.City.Should().Be(a.City);
            view.Line1.Should().Be(a.Line1);
            view.Line2.Should().Be(a.Line2);
            view.PostalCode.Should().Be(a.PostalCode);
            view.Country.Should().Be(a.Country);
        }
    }
}