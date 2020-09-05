using System;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using Xunit;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels
{
    public class RailwayViewTests
    {
        [Fact]
        public void RailwayView_ShouldRenderARailway_WithBasicInfo()
        {
            var expectedId = new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313");
            var view = RailwayViewWith(
                id: expectedId,
                periodOfActivity: PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)));

            view.Should().NotBeNull();
            view.Id.Should().Be(expectedId);
            view.Name.Should().Be("FS");
            view.Country.Should().Be("Italy");
            view.CompanyName.Should().Be("Ferrovie dello stato");
            view.WebsiteUrl.Should().Be("http://www.trenitalia.com/");
            view.Headquarters.Should().Be("Roma");

            view.PeriodOfActivity.Should().NotBeNull();
            view.PeriodOfActivity?.Status.Should().Be("Active");
            view.PeriodOfActivity?.OperatingSince.Should().NotBeNull();
            view.PeriodOfActivity?.OperatingUntil.Should().BeNull();
            view.TrackGauge.Should().BeNull();
            view.TotalLength.Should().BeNull();
        }

        [Fact]
        public void RailwayView_ShouldRenderARailway_WithTotalLength()
        {
            var view = RailwayViewWith(
                railwayLength: RailwayLength.Create(1000.123456M, 800.654321M));

            view.Should().NotBeNull();
            view.TotalLength.Should().NotBeNull();
            view.TotalLength?.Kilometers.Should().Be(1000M);
            view.TotalLength?.Miles.Should().Be(801M);
        }

        [Fact]
        public void RailwayView_ShouldRenderARailway_WithTrackGauge()
        {
            var view = RailwayViewWith(
                railwayGauge: RailwayGauge.Create(TrackGauge.Standard.ToString(), 56.49606M, 1435.000000M));

            view.Should().NotBeNull();
            view.TrackGauge.Should().NotBeNull();
            view.TrackGauge?.Millimeters.Should().Be(1435.0M);
            view.TrackGauge?.Inches.Should().Be(56.5M);
            view.TrackGauge?.Gauge.Should().Be("Standard");
        }

        private static RailwayView RailwayViewWith(
            Guid? id = null,
            RailwayLength railwayLength = null,
            RailwayGauge railwayGauge = null,
            PeriodOfActivity periodOfActivity = null)
        {
            var railway = CatalogSeedData.Railways.New()
                .Id(new RailwayId(id ?? new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")))
                .Name("FS")
                .CompanyName("Ferrovie dello stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(periodOfActivity)
                .RailwayGauge(railwayGauge)
                .RailwayLength(railwayLength)
                .WebsiteUrl(new Uri("http://www.trenitalia.com"))
                .Headquarters("Roma")
                .Build();
            return new RailwayView(railway, new LinksView());
        }
    }
}
