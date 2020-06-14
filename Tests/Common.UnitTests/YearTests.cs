using FluentAssertions;
using NodaTime;
using Xunit;

namespace TreniniDotNet.Common
{
    public class YearTests
    {
        [Fact]
        public void Year_ShouldCreateNewValues_FromLocalDates()
        {
            var year = Year.FromLocalDate(new LocalDate(1988, 11, 25));
            year.Should().Be(Year.Of(1988));
        }

        [Fact]
        public void Year_ShouldCheckEquality()
        {
            var year1 = Year.Of(1988);
            var year2 = Year.Of(1988);
            var year3 = Year.Of(2020);

            (year1 == year2).Should().BeTrue();
            (year1 == year3).Should().BeFalse();

            (year1 != year2).Should().BeFalse();
            (year1 != year3).Should().BeTrue();
        }
    }
}
