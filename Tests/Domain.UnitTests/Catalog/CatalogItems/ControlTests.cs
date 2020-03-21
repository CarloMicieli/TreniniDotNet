using Xunit;
using FluentAssertions;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class ControlTests
    {
        [Fact]
        public void Controls_ShouldCreateValue_FromValidStrings()
        {
            bool success = Controls.TryParse(Control.DccReady.ToString(), out var control);

            success.Should().BeTrue();
            control.Should().Be(Control.DccReady);
        }

        [Fact]
        public void Controls_ShouldCreateValue_FromValidStringsIgnoringCase()
        {
            bool success = Controls.TryParse(Control.DccReady.ToString().ToLower(), out var control);

            success.Should().BeTrue();
            control.Should().Be(Control.DccReady);
        }

        [Fact]
        public void Controls_ShouldFailToCreateValue_FromInvalidStrings()
        {
            bool success = Controls.TryParse("invalid", out var control);

            success.Should().BeFalse();
        }
    }
}