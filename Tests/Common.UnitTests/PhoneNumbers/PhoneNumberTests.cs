using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.PhoneNumbers
{
    public class PhoneNumberTests
    {
        [Fact]
        public void PhoneNumber_Of_ShouldCreateNewPhoneNumbers()
        {
            var phoneNumber = PhoneNumber.Of("+41 44 668 18 00");

            phoneNumber.Should().NotBeNull();
            phoneNumber.ToString().Should().Be("+41 44 668 18 00");
        }
    }
}
