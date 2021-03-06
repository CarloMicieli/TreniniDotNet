using System;
using System.Net.Mail;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public class ShopsFactoryTests
    {
        private ShopsFactory Factory { get; }

        public ShopsFactoryTests()
        {
            Factory = new ShopsFactory(
                FakeClock.FromUtc(1988, 11, 25),
                FakeGuidSource.NewSource(new Guid("4ea17513-409b-4bda-9ffd-54e32f0e916a")));
        }

        [Fact]
        public void ShopsFactory_ShouldCreateNewValues()
        {
            var expectedAddress = Address.With(
                line1: "Marie-Curie-Stra�e 1",
                postalCode: "32760",
                city: "Detmold",
                country: "DE");

            var shop = Factory.CreateShop(
                "Modellbahnshop Lippe",
                new Uri("https://www.modellbahnshop-lippe.com"),
                new MailAddress("kundenservice@mail.modellbahnshop-lippe.com"),
                expectedAddress,
                PhoneNumber.Of("+49 5231 9807 123"));

            shop.Should().NotBeNull();
            shop.Id.Should().Be(new ShopId(new Guid("4ea17513-409b-4bda-9ffd-54e32f0e916a")));
            shop.Name.Should().Be("Modellbahnshop Lippe");
            shop.Address.Should().Be(expectedAddress);
            shop.PhoneNumber.Should().Be(PhoneNumber.Of("+49 5231 9807 123"));
            shop.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0));
            shop.Version.Should().Be(1);
        }
    }
}
