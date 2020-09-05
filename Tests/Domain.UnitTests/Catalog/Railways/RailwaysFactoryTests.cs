using System;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwaysFactoryTests
    {
        private static Instant _expectedDate = Instant.FromUtc(1988, 11, 25, 0, 0);
        private static RailwayId _expectedRailwayId = new RailwayId(new Guid("fab083e5-7c33-4276-9068-e2de39c30281"));

        private RailwaysFactory Factory { get; }

        public RailwaysFactoryTests()
        {
            var guidSource = FakeGuidSource.NewSource(_expectedRailwayId);
            Factory = new RailwaysFactory(new FakeClock(_expectedDate), guidSource);
        }

        [Fact]
        public void RailwaysFactory_ShouldCreateNewRailways()
        {
            var expectedPeriodOfActivity = PeriodOfActivity.ActiveRailway(new DateTime(1901, 11, 25));
            var expectedGauge = RailwayGauge.Create(TrackGauge.Standard.ToString(), null, 1435M);
            var expectedLength = RailwayLength.Create(10000M, null);

            var railway = Factory.CreateRailway(
                "name",
                "company name",
                Country.Of("DE"),
                expectedPeriodOfActivity,
                expectedLength,
                expectedGauge,
                new Uri("https://www.site.com"),
                "Rome");

            railway.Id.Should().Be(_expectedRailwayId);
            railway.Name.Should().Be("name");
            railway.Slug.Should().Be(Slug.Of("name"));
            railway.CompanyName.Should().Be("company name");
            railway.Country.Should().Be(Country.Of("DE"));
            railway.PeriodOfActivity.Should().Be(expectedPeriodOfActivity);
            railway.TotalLength.Should().Be(expectedLength);
            railway.Headquarters.Should().Be("Rome");
            railway.WebsiteUrl.Should().Be(new Uri("https://www.site.com"));
            railway.CreatedDate.Should().Be(_expectedDate);
            railway.ModifiedDate.Should().BeNull();
            railway.Version.Should().Be(1);
        }
    }
}
