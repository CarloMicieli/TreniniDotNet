using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class ScaleView
    {
        public ScaleView(IScale scale, LinksView? selfLink)
        {
            Links = selfLink;

            Id = scale.ScaleId.ToGuid();
            Name = scale.Name;
            Ratio = scale.Ratio.ToDecimal();

            Gauge = new ScaleGaugeView
            {
                TrackGauge = scale.Gauge.TrackGauge.ToString(),
                Millimeters = scale.Gauge.InMillimeters.Value,
                Inches = scale.Gauge.InInches.Value
            };

            Standards = new List<string>();
            Weight = scale.Weight;
            Description = scale.Description;
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { get; }

        public Guid Id { get; }

        public string Name { get; }

        public decimal? Ratio { get; }

        public ScaleGaugeView? Gauge { get; }

        public string? Description { get; }

        public int? Weight { get; }

        public List<string> Standards { get; }
    }

    public class ScaleGaugeView
    {
        public decimal? Millimeters { set; get; }
        public decimal? Inches { set; get; }
        public string? TrackGauge { set; get; }
    }
}