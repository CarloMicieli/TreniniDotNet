using System;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class ScaleView
    {
        public ScaleView(IScale scale)
        {
            Id = scale.ScaleId.ToGuid();
            Slug = scale.Slug.ToString();
            Name = scale.Name;
            Ratio = scale.Ratio.ToDecimal();
            Gauge = scale.Gauge.ToDecimal(Domain.Catalog.ValueObjects.MeasureUnit.Millimeters);
            TrackGauge = scale.TrackGauge.ToString();
        }

        public Guid Id { get; }
        
        public string Slug { get; }
        
        public string Name { get; } 
        
        public decimal? Ratio { get; }
        
        public decimal? Gauge { get; }

        public string? TrackGauge { get; }
    }
}