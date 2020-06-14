using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.DeliveryDates
{
    public class DeliveryDateTests
    {
        [Fact]
        public void DeliveryDate_ShouldCreateValue_FromYearAndQuarter()
        {
            bool result = DeliveryDate.TryCreate(2020, Quarter.Q1, out var dd);

            result.Should().BeTrue();
            dd.Quarter.Should().Be(Quarter.Q1);
            dd.Year.Should().Be(2020);
        }

        [Fact]
        public void DeliveryDate_ShouldProductAStringRepresentation()
        {
            DD(2020, Quarter.Q1).ToString().Should().Be("2020/Q1");
            DD(2020, null).ToString().Should().Be("2020");
        }

        [Fact]
        public void DeliveryDate_ShouldParseString_WhenTheyAreValid()
        {
            bool result1 = DeliveryDate.TryParse(DD(1988, Quarter.Q4).ToString(), out var dd1);
            bool result2 = DeliveryDate.TryParse(DD(1988, null).ToString(), out var dd2);

            result1.Should().BeTrue();
            dd1.Value.Quarter.Should().Be(Quarter.Q4);
            dd1.Value.Year.Should().Be(1988);

            result2.Should().BeTrue();
            dd2.Value.Year.Should().Be(1988);
        }

        [Fact]
        public void DeliveryDate_ShouldCheckEquality()
        {
            var d1 = DD(2020, Quarter.Q1);
            var d2 = DD(2020, Quarter.Q4);
            var d3 = DD(2019, Quarter.Q1);
            var d4 = DD(2020, null);

            (d1.Equals(d1)).Should().Be(true);
            (d1.Equals(d2)).Should().Be(false);
            (d1.Equals(d3)).Should().Be(false);
            (d1.Equals(d4)).Should().Be(false);
        }

        [Fact]
        public void DeliveryDate_ShouldCheckEquality_WithEqualOperator()
        {
            (DD(2020, Quarter.Q1) == DD(2020, Quarter.Q1)).Should().Be(true);
            (DD(2020, Quarter.Q1) != DD(2020, Quarter.Q1)).Should().Be(false);

            (DD(2020, Quarter.Q1) == DD(2020, Quarter.Q2)).Should().Be(false);
            (DD(2020, Quarter.Q1) != DD(2020, Quarter.Q2)).Should().Be(true);

            (DD(2020, Quarter.Q1) == DD(2019, Quarter.Q1)).Should().Be(false);
            (DD(2020, Quarter.Q1) != DD(2019, Quarter.Q1)).Should().Be(true);

            (DD(2020, Quarter.Q1) == DD(2020, null)).Should().Be(false);
            (DD(2020, Quarter.Q1) != DD(2020, null)).Should().Be(true);
        }

        [Fact]
        public void DeliveryDate_ShouldCompareTwoDeliveryDates()
        {
            var d1 = DD(2020, Quarter.Q1);
            var d2 = DD(2020, Quarter.Q4);
            var d3 = DD(2019, Quarter.Q1);

            (d1.CompareTo(d1)).Should().Be(0);
            (d1.CompareTo(d2)).Should().BeNegative();
            (d1.CompareTo(d3)).Should().BePositive();
        }

        [Fact]
        public void DeliveryDate_ShouldCompareTwoDeliveryDates_WithoutQuarter()
        {
            var d1 = DD(2020, Quarter.Q1);
            var d2 = DD(2020, null);
            var d3 = DD(2019, null);

            (d1.CompareTo(d2)).Should().BeNegative();
            (d1.CompareTo(d3)).Should().BePositive();
            (d2.CompareTo(d3)).Should().BePositive();
        }

        [Fact]
        public void DeliveryDate_ShouldCompareValues_UsingTheOperators()
        {
            (DD(2020, Quarter.Q1) < DD(2020, Quarter.Q2)).Should().Be(true);
            (DD(2020, Quarter.Q1) >= DD(2020, Quarter.Q2)).Should().Be(false);

            (DD(2020, Quarter.Q2) > DD(2020, Quarter.Q1)).Should().Be(true);
            (DD(2020, Quarter.Q2) <= DD(2020, Quarter.Q1)).Should().Be(false);

            (DD(2020, null) > DD(2020, Quarter.Q1)).Should().Be(true);
            (DD(2020, null) <= DD(2020, Quarter.Q1)).Should().Be(false);

            (DD(2020, Quarter.Q1) < DD(2020, null)).Should().Be(true);
            (DD(2020, Quarter.Q1) >= DD(2020, null)).Should().Be(false);

            (DD(2020, null) > DD(2019, null)).Should().Be(true);
            (DD(2020, null) <= DD(2019, null)).Should().Be(false);

            (DD(2019, null) < DD(2020, null)).Should().Be(true);
            (DD(2019, null) >= DD(2020, null)).Should().Be(false);
        }

        private static DeliveryDate DD(int year, Quarter? q)
        {
            bool _ = DeliveryDate.TryCreate(year, q, out var dd);
            return dd;
        }
    }
}