using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Addresses
{
    public class AddressTests
    {
        [Fact]
        public void Address_With_CreateAddresses()
        {
            var a = Address.With(
                line1: "22 Acacia Avenue",
                line2: "West End",
                city: "London",
                postalCode: "12345",
                country: "UK");

            a.Should().NotBeNull();
            a.Line1.Should().Be("22 Acacia Avenue");
            a.Line2.Should().Be("West End");
            a.City.Should().Be("London");
            a.PostalCode.Should().Be("12345");
            a.Country.Should().Be("UK");
        }

        [Fact]
        public void Address_TryCreate_CreateAddresses()
        {
            var result = Address.TryCreate(
                "22 Acacia Avenue",
                "West End",
                "London",
                null,
                "12345",
                "UK",
                out var a);

            a.Should().NotBeNull();
            a.Line1.Should().Be("22 Acacia Avenue");
            a.Line2.Should().Be("West End");
            a.Region.Should().BeNull();
            a.City.Should().Be("London");
            a.PostalCode.Should().Be("12345");
            a.Country.Should().Be("UK");
        }

        [Fact]
        public void Address_Equals_ShouldReturnTrue_WhenAddressesAreEquals()
        {
            var (left, right) = TwoEqualsAddresses();
            (left.Equals(right)).Should().BeTrue();
            (left == right).Should().BeTrue();
            (left != right).Should().BeFalse();
        }

        [Fact]
        public void Address_ToString_ShouldProduceStringRepresentations()
        {
            var address = NewAddress();
            address.ToString().Should().Be("address line 1, address line 2, city name, region, postal code, IT");
        }

        private static (Address left, Address right) TwoEqualsAddresses()
        {
            return (NewAddress(), NewAddress());
        }

        private static Address NewAddress() => Address.With(
                "address line 1", "address line 2",
                "city name",
                "region",
                "postal code",
                "IT");
    }
}
