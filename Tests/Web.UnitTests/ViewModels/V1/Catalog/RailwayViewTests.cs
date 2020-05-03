using Xunit;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class RailwayViewTests
    {
        [Fact]
        public void RailwayView_ShouldRenderARailway_WithBasicInfo()
        {
            var expectedId = new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313");
            var view = RailwayViewWith(id: expectedId);

            view.Should().NotBeNull();
            view.Id.Should().Be(expectedId);
            view.Name.Should().Be("FS");
            view.Country.Should().Be("Italy");
            view.CompanyName.Should().Be("Ferrovie dello stato");
            view.WebsiteUrl.Should().Be("http://www.trenitalia.com/");
            view.Headquarters.Should().Be("Roma");

            view.PeriodOfActivity.Should().NotBeNull();
            view.PeriodOfActivity.Status.Should().Be("Active");
            view.PeriodOfActivity.OperatingSince.Should().BeNull();
            view.PeriodOfActivity.OperatingUntil.Should().BeNull();
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
            view.TotalLength.Kilometers.Should().Be(1000M);
            view.TotalLength.Miles.Should().Be(801M);
        }

        [Fact]
        public void RailwayView_ShouldRenderARailway_WithTrackGauge()
        {
            var view = RailwayViewWith(
                railwayGauge: RailwayGauge.Create(TrackGauge.Standard.ToString(), 56.49606M, 1435.000000M));

            view.Should().NotBeNull();
            view.TrackGauge.Should().NotBeNull();
            view.TrackGauge.Millimeters.Should().Be(1435.0M);
            view.TrackGauge.Inches.Should().Be(56.5M);
            view.TrackGauge.Gauge.Should().Be("Standard");
        }

        private static RailwayView RailwayViewWith(
            Guid? id = null,
            RailwayLength railwayLength = null,
            RailwayGauge railwayGauge = null,
            PeriodOfActivity periodOfActivity = null)
        {
            var railway = CatalogSeedData.NewRailwayWith(
                id: new RailwayId(id ?? new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313")),
                name: "FS",
                companyName: "Ferrovie dello stato",
                country: Country.Of("IT"),
                periodOfActivity: periodOfActivity,
                gauge: railwayGauge,
                railwayLength: railwayLength,
                websiteUrl: new Uri("http://www.trenitalia.com"),
                headquarters: "Roma");
            return new RailwayView(railway, new LinksView());
        }
    }
}
