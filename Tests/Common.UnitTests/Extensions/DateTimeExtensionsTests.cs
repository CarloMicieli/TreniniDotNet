using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using Xunit;

namespace TreniniDotNet.Common.Extensions
{
    public class DateTimeExtensionsTests
    {
        private readonly Instant _currentInstant;
        private readonly IClock _clock;

        public DateTimeExtensionsTests()
        {
            _currentInstant = Instant.FromUtc(1988, 11, 25, 10, 30);
            _clock = new FakeClock(_currentInstant);
        }

        [Fact]
        public void DateTime_ToUtcOrGetCurrent_ShouldReturnTheValueAsUtc_WhenItIsNotNull()
        {
            DateTime? dateValue = new DateTime(2020, 11, 25, 10, 30, 0);
            Instant instant = dateValue.ToUtcOrGetCurrent(_clock);

            instant.Should().Be(Instant.FromUtc(2020, 11, 25, 10, 30));
        }

        [Fact]
        public void DateTime_ToUtcOrGetCurrent_ShouldReturnCurrentInstant_WhenItIsNull()
        {
            DateTime? dateValue = null;
            Instant instant = dateValue.ToUtcOrGetCurrent(_clock);

            instant.Should().Be(_currentInstant);
        }
    }
}
