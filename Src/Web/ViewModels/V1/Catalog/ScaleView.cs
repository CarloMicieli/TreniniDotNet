using System;
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
            Gauge = scale.Gauge.ToDecimal(Domain.Catalog.ValueObjects.MeasureUnit.Millimeters);
            TrackGauge = scale.TrackGauge.ToString();
        }

        [JsonPropertyName("_links")]
        public LinksView? Links { get; }

        public Guid Id { get; }

        public string Name { get; }

        public decimal? Ratio { get; }

        public decimal? Gauge { get; }

        public string? TrackGauge { get; }
    }
}