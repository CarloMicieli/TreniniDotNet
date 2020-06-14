using System;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Railways.Common.ViewModels
{
    public sealed class RailwayView
    {
        public RailwayView(IRailway railway, LinksView? selfLink)
        {
            this.Links = selfLink;
            this.Id = railway.Id.ToGuid();
            this.Name = railway.Name;
            this.CompanyName = railway.CompanyName;
            this.Country = railway.Country.ToString();
            this.WebsiteUrl = railway.WebsiteUrl?.ToString();
            this.Headquarters = railway.Headquarters;

            this.PeriodOfActivity = new PeriodOfActivityView
            {
                Status = railway.PeriodOfActivity.RailwayStatus.ToString(),
                OperatingSince = railway.PeriodOfActivity.OperatingSince,
                OperatingUntil = railway.PeriodOfActivity.OperatingUntil
            };

            if (!(railway.TotalLength is null))
            {
                this.TotalLength = new TotalLengthView(railway.TotalLength);
            }

            if (!(railway.TrackGauge is null))
            {
                this.TrackGauge = new RailwayGaugeView(railway.TrackGauge);
            }
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { set; get; }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? Country { get; set; }

        public PeriodOfActivityView? PeriodOfActivity { get; set; }

        public TotalLengthView? TotalLength { get; set; }

        public RailwayGaugeView? TrackGauge { get; set; }

        public string? Headquarters { get; set; }

        public string? WebsiteUrl { get; set; }
    }

    public class PeriodOfActivityView
    {
        public string? Status { set; get; }
        public DateTime? OperatingSince { set; get; }
        public DateTime? OperatingUntil { set; get; }
    }

    public class TotalLengthView
    {
        public TotalLengthView(RailwayLength railwayLength)
        {
            Miles = decimal.Round(railwayLength.Miles.Value, 0);
            Kilometers = decimal.Round(railwayLength.Kilometers.Value, 0);
        }

        public decimal? Miles { get; }
        public decimal? Kilometers { get; }
    }

    public class RailwayGaugeView
    {
        public RailwayGaugeView(RailwayGauge rg)
        {
            Millimeters = decimal.Round(rg.Millimeters.Value, 1);
            Inches = decimal.Round(rg.Inches.Value, 1);
            Gauge = rg.TrackGauge.ToString();
        }

        public decimal? Millimeters { get; }
        public decimal? Inches { get; }
        public string? Gauge { get; }
    }
}
