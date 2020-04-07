using System.Collections.Generic;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public sealed class CreateScaleInput : IUseCaseInput
    {
        public CreateScaleInput(string? name, decimal? ratio, decimal? gauge, string? trackGauge, string? notes)
        {
            Name = name;
            Ratio = ratio;
            Gauge = gauge;
            TrackGauge = trackGauge;
            Notes = notes;
            Weight = null; //TODO: fixme
        }

        public string? Name { get; }

        public decimal? Ratio { get; }

        public decimal? Gauge { get; }

        public string? TrackGauge { get; }

        public string? Notes { get; }

        public List<string> Standards { get; } = new List<string>();

        public int? Weight { get; }
    }

    public sealed class ScaleGaugeInput
    {
        public string? TrackGauge { set; get; }
        public decimal? Inches { set; get; }
        public decimal? Millimeters { set; get; }
    }
}