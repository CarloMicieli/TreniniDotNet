using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Common
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

        //[Fact]
        //public void Country_ValidCountryCode_ShouldConvertValidCodes()
        //{
        //    var result = Country.ValidCountryCode("Jp");
        //    result.Match(
        //        Succ: japan =>
        //        {
        //            japan.Should().NotBeNull();
        //            japan.Code.Should().Be("JP");
        //            japan.EnglishName.Should().Be("Japan");
        //        },
        //        Fail: errors => Assert.True(false, "it should never arrive here"));
        //}

        //[Fact]
        //public void Country_ValidCountryCode_ShouldFailToConvertInvalidCodes()
        //{
        //    var result = Country.ValidCountryCode("Uk");
        //    result.Match(
        //        Succ: country => Assert.True(false, "it should never arrive here"),
        //        Fail: errors =>
        //        {
        //            var errorsList = errors.ToList();
        //            errorsList.Should().HaveCount(1);
        //            errorsList.Should().Contain(Error.New("'Uk' is not a valid country code."));
        //        });
        //}

        //[Fact]
        //public void Country_ValidCountryCode_ShouldFailToConvertNull()
        //{
        //    var result = Country.ValidCountryCode(null);
        //    result.Match(
        //        Succ: country => Assert.True(false, "it should never arrive here"),
        //        Fail: errors =>
        //        {
        //            var errorsList = errors.ToList();
        //            errorsList.Should().HaveCount(1);
        //            errorsList.Should().Contain(Error.New("'null' is not a valid country code."));
        //        });
        //}
    }
}