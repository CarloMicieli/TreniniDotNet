using FluentAssertions;
using Xunit;

namespace TreniniDotNet.SharedKernel.Countries
{
    public class CountryTests
    {
        [Fact]
        public void Country_Of_ShouldCreateNewCountries()
        {
            Country japan = Country.Of("Jp");

            japan.Should().NotBeNull();
            japan.Code.Should().Be("JP");
            japan.EnglishName.Should().Be("Japan");
        }

        [Fact]
        public void Country_Equals_ShouldCheckCountriesEquality()
        {
            Country japan1 = Country.Of("Jp");
            Country japan2 = Country.Of("Jp");
            Country switzerland = Country.Of("CH");

            japan1.Equals(japan2).Should().BeTrue();
            (japan1 == japan2).Should().BeTrue();
            japan1.Equals(switzerland).Should().BeFalse();
            (japan1 != switzerland).Should().BeTrue();
        }

    }
}
