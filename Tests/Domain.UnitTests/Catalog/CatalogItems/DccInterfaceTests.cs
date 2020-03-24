using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class DccInterfaceTests
    {
        [Fact]
        public void DccInterfaces_ShouldCreateValue_FromValidStrings()
        {
            bool result = DccInterfaces.TryParse(DccInterface.Nem652.ToString(), out var dcc);

            result.Should().BeTrue();
            dcc.Should().Be(DccInterface.Nem652);
        }

        [Fact]
        public void DccInterfaces_ShouldCreateValue_FromValidStringsIgnoringCase()
        {
            bool result = DccInterfaces.TryParse(DccInterface.Nem652.ToString().ToUpper(), out var dcc);

            result.Should().BeTrue();
            dcc.Should().Be(DccInterface.Nem652);
        }

        [Fact]
        public void DccInterfaces_ShouldFailToCreateValues_FromInvalidStrings()
        {
            bool failure = DccInterfaces.TryParse("     ", out var _);
            failure.Should().BeFalse();
        }

        [Fact]
        public void DccInterfaces_ShouldCreateTheNoneValue_WhenStringIsNull()
        {
            bool success = DccInterfaces.TryParse(null, out var result);
            success.Should().BeTrue();
            result.Should().Be(DccInterface.None);
        }
    }
}