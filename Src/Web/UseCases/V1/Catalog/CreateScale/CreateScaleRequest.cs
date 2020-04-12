using MediatR;
using System.Collections.Generic;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleRequest : IRequest
    {
        public string? Name { set; get; }
        public decimal? Ratio { set; get; }
        public ScaleGaugeRequest? Gauge { set; get; }
        public string? Description { set; get; }
        public int? Weight { get; }
        public List<string> Standards { get; set; } = new List<string>();
    }

    public sealed class ScaleGaugeRequest
    {
        public string? TrackGauge { set; get; }
        public decimal? Inches { set; get; }
        public decimal? Millimeters { set; get; }
    }
}