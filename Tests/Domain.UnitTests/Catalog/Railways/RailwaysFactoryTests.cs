using Xunit;
using FluentAssertions;
using System;
using NodaTime.Testing;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwaysFactoryTests
    {
        private IRailwaysFactory Factory { get; }
        private RailwayId ExpectedRailwayId = new RailwayId(new Guid("00545b98-8629-4e9d-8880-3f473296cb9a"));
        private Instant ExpectedDateTime = Instant.FromUtc(1988, 11, 25, 0, 0);

        public RailwaysFactoryTests()
        {
            this.Factory = new RailwaysFactory(
                new FakeClock(ExpectedDateTime),
                FakeGuidSource.NewSource(ExpectedRailwayId.ToGuid()));
        }

        [Fact]
        public void RailwaysFactory_CreateNewRailway_ShouldCreateNewRailways()
        {
            var ExpectedPeriodOfActivity = PeriodOfActivity.ActiveRailway(new DateTime(1901, 11, 25));
            var ExpectedGauge = RailwayGauge.Create(TrackGauge.Standard.ToString(), null, 1435M);
            var ExpectedLength = RailwayLength.Create(10000M, null);

            IRailway railway = Factory.CreateNewRailway(
               "name",
                "company name",
                Country.Of("DE"),
                ExpectedPeriodOfActivity,
                ExpectedLength,
                ExpectedGauge,
                new Uri("https://www.site.com"),
                "Rome");

            railway.RailwayId.Should().Be(ExpectedRailwayId);
            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("name"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be(Country.Of("DE"));
            railway.PeriodOfActivity.Should().Be(ExpectedPeriodOfActivity);
            railway.TotalLength.Should().Be(ExpectedLength);
            railway.Headquarters.Should().Be("Rome");
            railway.WebsiteUrl.Should().Be(new Uri("https://www.site.com"));
            railway.CreatedDate.Should().Be(ExpectedDateTime);
            railway.ModifiedDate.Should().BeNull();
            railway.Version.Should().Be(1);
        }

        [Fact]
        public void RailwaysFactory_NewRailway_ShouldCreateNewRailways()
        {
            var ExpectedPeriodOfActivity = PeriodOfActivity.ActiveRailway(new DateTime(1901, 11, 25));
            var ExpectedGauge = RailwayGauge.Create(TrackGauge.Standard.ToString(), null, 1435M);
            var ExpectedLength = RailwayLength.Create(10000M, null);

            IRailway railway = Factory.NewRailway(
                ExpectedRailwayId.ToGuid(),
                "name",
                "name",
                "company name",
                "DE",
                new DateTime(1901, 11, 25),
                null,
                true,
                ExpectedGauge.Millimeters.Value,
                ExpectedGauge.Inches.Value,
                ExpectedGauge.TrackGauge.ToString(),
                "Rome",
                6213.71M,
                10000M,
                "https://www.site.com",
                ExpectedDateTime.ToDateTimeUtc(),
                ExpectedDateTime.ToDateTimeUtc(),
                2);

            railway.RailwayId.Should().Be(ExpectedRailwayId);
            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("name"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be(Country.Of("DE"));
            railway.PeriodOfActivity.Should().Be(ExpectedPeriodOfActivity);
            railway.TotalLength.Should().Be(ExpectedLength);
            railway.Headquarters.Should().Be("Rome");
            railway.WebsiteUrl.Should().Be(new Uri("https://www.site.com"));
            railway.CreatedDate.Should().Be(ExpectedDateTime);
            railway.ModifiedDate.Should().Be(ExpectedDateTime);
            railway.Version.Should().Be(2);
        }

        [Fact]
        public void RailwaysFactory_UpdateRailway_ShouldApplyRailwayChanges()
        {
            var fs = CatalogSeedData.Railways.Fs();

            var modified = Factory.UpdateRailway(fs,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "Modified");

            modified.Headquarters.Should().Be("Modified");
            modified.ModifiedDate.Should().Be(ExpectedDateTime);
            modified.Version.Should().Be(2);
        }
    }
}