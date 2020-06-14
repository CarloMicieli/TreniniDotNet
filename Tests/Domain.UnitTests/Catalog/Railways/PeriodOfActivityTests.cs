using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class PeriodOfActivityTests
    {
        private static DateTime ExpectedSince => new DateTime(1988, 11, 25);
        private static DateTime ExpectedUntil => new DateTime(2020, 1, 1);

        [Fact]
        public void PeriodOfActivity_ActiveRailway_ShouldCreatePeriodsForActiveRailways()
        {
            var period = PeriodOfActivity.ActiveRailway(ExpectedSince);

            period.Should().NotBeNull();
            period.RailwayStatus.Should().Be(RailwayStatus.Active);
            period.OperatingSince.Should().Be(ExpectedSince);
            period.OperatingUntil.Should().BeNull();
        }

        [Fact]
        public void PeriodOfActivity_InactiveRailway_ShouldCreatePeriodsForInactiveRailways()
        {
            var period = PeriodOfActivity.InactiveRailway(ExpectedSince, ExpectedUntil);

            period.Should().NotBeNull();
            period.RailwayStatus.Should().Be(RailwayStatus.Inactive);
            period.OperatingSince.Should().Be(ExpectedSince);
            period.OperatingUntil.Should().Be(ExpectedUntil);
        }

        [Fact]
        public void PeriodOfActivity_Of_ShouldCreatePeriodForInactiveRailways()
        {
            var period = PeriodOfActivity.Of("Inactive", ExpectedSince, ExpectedUntil);

            period.Should().NotBeNull();
            period.RailwayStatus.Should().Be(RailwayStatus.Inactive);
            period.OperatingSince.Should().Be(ExpectedSince);
            period.OperatingUntil.Should().Be(ExpectedUntil);
        }

        [Fact]
        public void PeriodOfActivity_Of_ShouldCreatePeriodForActiveRailways()
        {
            var period = PeriodOfActivity.Of("Active", ExpectedSince, null);

            period.Should().NotBeNull();
            period.RailwayStatus.Should().Be(RailwayStatus.Active);
            period.OperatingSince.Should().Be(ExpectedSince);
            period.OperatingUntil.Should().BeNull();
        }

        [Fact]
        public void PeriodOfActivity_Of_ShouldValidateValues()
        {
            Action act1 = () => PeriodOfActivity.Of("Inactive", ExpectedSince, null);
            Action act2 = () => PeriodOfActivity.Of("active", ExpectedSince, ExpectedUntil);
            Action act3 = () => PeriodOfActivity.Of("Inactive", ExpectedUntil, ExpectedSince);

            act1.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid period of activity: operatingUntil is required for an inactive railway");

            act2.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid period of activity: operatingUntil has a value for an active railway");

            act3.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid period of activity: operatingSince > operatingUntil");
        }

        [Fact]
        public void PeriodOfActivity_TryCreate_ShouldValidateValues()
        {
            bool res1 = PeriodOfActivity.TryCreate("Inactive", ExpectedSince, null, out var p1);
            bool res2 = PeriodOfActivity.TryCreate("active", ExpectedSince, ExpectedUntil, out var p2);
            bool res3 = PeriodOfActivity.TryCreate("Inactive", ExpectedUntil, ExpectedSince, out var p3);

            res1.Should().BeFalse();
            res2.Should().BeFalse();
            res3.Should().BeFalse();
        }
    }
}
