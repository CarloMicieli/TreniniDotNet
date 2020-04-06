using Xunit;
using FluentAssertions;
using LanguageExt;

namespace TreniniDotNet.Common.Addresses
{
    public class AddressTests
    {
        [Fact]
        public void Address_TryCreate_ShouldFailToCreateInvalidAddresses()
        {
            Validation<Error, Address> result = Address.TryCreate(null, null, null, null, null, null);
            result.Match(
                Succ: succ => Assert.True(false, "It should never arrive here"),
                Fail: errors =>
                {
                    var errorsList = errors.ToList();

                    errorsList.Should().HaveCount(4);
                    errorsList.Should().ContainInOrder(
                        Error.New("invalid address: the first line is required"),
                        Error.New("invalid address: the town/city is required"),
                        Error.New("invalid address: the postal code is required"),
                        Error.New("invalid address: the country is required")
                        );
                });
        }

        [Fact]
        public void Address_TryCreate_ShouldCreateAddressesFromValidValues()
        {
            Validation<Error, Address> result = Address.TryCreate(
                "address line 1", "address line 2",
                "city name",
                "region",
                "postal code",
                "IT");

            result.Match(
                Succ: address =>
                {
                    address.Line1.Should().Be("address line 1");
                    address.Line2.Should().Be("address line 2");
                    address.City.Should().Be("city name");
                    address.Region.Should().Be("region");
                    address.PostalCode.Should().Be("postal code");
                    address.Country.Should().Be("IT");
                },
                Fail: errors => Assert.True(false, "It should never arrive here"));
        }

        [Fact]
        public void AddressEquals_ShouldReturnTrue_WhenAddressesAreEquals()
        {
            var (left, right) = TwoEqualsAddresses();
            (left.Equals(right)).Should().BeTrue();
            (left == right).Should().BeTrue();
        }

        private static (Address left, Address right) TwoEqualsAddresses()
        {
            return (NewAddress(), NewAddress());
        }

        private static Address NewAddress()
        {
            return new Address(
                "address line 1", "address line 2",
                "city name",
                "region",
                "postal code",
                "IT");
        }
    }
}