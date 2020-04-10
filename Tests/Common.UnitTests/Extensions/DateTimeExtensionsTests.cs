using Xunit;
using FluentAssertions;
using System;
using NodaTime;
using NodaTime.Testing;

namespace TreniniDotNet.Common.Extensions
{
    public class DateTimeExtensionsTests
    {
        private readonly IClock clock;

        public DateTimeExtensionsTests()
        {
            clock = FakeClock.FromUtc(1988, 11, 25, 10, 0, 0);
        }

        [Fact]
        public void DateTime_ToUtcOrGetCurrent_ShouldGetTheValue()
        {
            DateTime? date = new DateTime(2020, 11, 25, 10, 0, 0);
            
            var result = date.ToUtcOrGetCurrent(clock);

            result.Should().Be(Instant.FromUtc(2020, 11, 25, 10, 0, 0));
        }

        [Fact]
        public void DateTime_ToUtcOrGetCurrent_ShouldGetCurrentValueFromClock()
        {
            DateTime? date = null;

            var result = date.ToUtcOrGetCurrent(clock);

            result.Should().Be(Instant.FromUtc(1988, 11, 25, 10, 0, 0));
        }
    }
}
