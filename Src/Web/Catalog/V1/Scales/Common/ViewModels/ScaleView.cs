using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels
{
    public sealed class ScaleView
    {
        public ScaleView(IScale scale, LinksView? selfLink)
        {
            Links = selfLink;

            Id = scale.Id.ToGuid();
            Name = scale.Name;
            Ratio = scale.Ratio.ToDecimal();

            Gauge = new ScaleGaugeView(scale.Gauge);

            Standards = new List<string>();
            Weight = scale.Weight;
            Description = scale.Description;
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { get; }

        public Guid Id { get; }

        public string Name { get; }

        public decimal? Ratio { get; }

        public ScaleGaugeView Gauge { get; }

        public string? Description { get; }

        public int? Weight { get; }

        public List<string> Standards { get; }
    }

    public class ScaleGaugeView
    {
        public ScaleGaugeView(ScaleGauge scaleGauge)
        {
            TrackGauge = scaleGauge.TrackGauge.ToString();
            Millimeters = decimal.Round(scaleGauge.InMillimeters.Value, 2);
            Inches = decimal.Round(scaleGauge.InInches.Value, 2);
        }

        public decimal? Millimeters { get; }
        public decimal? Inches { get; }
        public string? TrackGauge { get; }
    }
}
