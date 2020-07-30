using System;
using System.Net.Mail;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public class ShopTests
    {
        [Fact]
        public void Shop_ShouldCreateNewValues()
        {
            var id = ShopId.NewId();
            var expectedAddress = Address.With(
                line1: "Marie-Curie-Stra�e 1",
                postalCode: "32760",
                city: "Detmold",
                country: "DE");

            var shop = new Shop(
                id,
                "name",
                new Uri("http://www.name.com"),
                new MailAddress("mail@name.com"),
                expectedAddress,
                PhoneNumber.Of("+39 555 1234"),
                Instant.FromUtc(1988, 11, 25, 10, 30),
                null,
                1);

            shop.Should().NotBeNull();
            shop.Id.Should().Be(id);
            shop.Address.Should().Be(expectedAddress);
            shop.Name.Should().Be("name");
            shop.Slug.Should().Be(Slug.Of("name"));
            shop.PhoneNumber.Should().Be(PhoneNumber.Of("+39 555 1234"));
            shop.CreatedDate.Should().Be(Instant.FromUtc(1988, 11, 25, 10, 30));
            shop.ModifiedDate.Should().BeNull();
            shop.Version.Should().Be(1);
        }

        [Fact]
        public void Shop_ShouldCheckForEquality()
        {
            var shop1 = CollectingSeedData.Shops.NewTecnomodelTreni();
            var shop2 = CollectingSeedData.Shops.NewTecnomodelTreni();

            (shop1 == shop2).Should().BeTrue();
            (shop1 != shop2).Should().BeFalse();
            shop1.Equals(shop2).Should().BeTrue();
        }

        [Fact]
        public void Shop_ShouldCheckForInequality()
        {
            var shop1 = CollectingSeedData.Shops.NewTecnomodelTreni();
            var shop2 = CollectingSeedData.Shops.NewModellbahnshopLippe();

            (shop1 == shop2).Should().BeFalse();
            (shop1 != shop2).Should().BeTrue();
            shop1.Equals(shop2).Should().BeFalse();
        }
    }
}
