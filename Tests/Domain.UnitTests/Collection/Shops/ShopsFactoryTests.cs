using Xunit;
using FluentAssertions;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using System;
using System.Net.Mail;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collection.ValueObjects;
using NodaTime;
using TreniniDotNet.Common.Addresses;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public class ShopsFactoryTests
    {
        private IShopsFactory Factory { get; }

        public ShopsFactoryTests()
        {
            Factory = new ShopsFactory(
                FakeClock.FromUtc(1988, 11, 25),
                FakeGuidSource.NewSource(new Guid("4ea17513-409b-4bda-9ffd-54e32f0e916a")));
        }

        [Fact]
        public void ShopsFactory_NewShop_ShouldCreateNewShops()
        {
            var shop = Factory.NewShop(
                "Modellbahnshop Lippe",
                new Uri("https://www.modellbahnshop-lippe.com"),
                new MailAddress("kundenservice@mail.modellbahnshop-lippe.com"),
                Address.With(line1: "Marie-Curie-Straﬂe 1", postalCode: "32760", city: "Detmold", country: "DE"),
                PhoneNumber.Of("+49 5231 9807 123"));

            shop.Should().NotBeNull();
            shop.ShopId.Should().Be(new ShopId(new Guid("4ea17513-409b-4bda-9ffd-54e32f0e916a")));
            shop.Name.Should().Be("Modellbahnshop Lippe");
            shop.PhoneNumber.Should().Be(PhoneNumber.Of("+49 5231 9807 123"));
            shop.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 0, 0));
            shop.Version.Should().Be(1);
        }
    }
}