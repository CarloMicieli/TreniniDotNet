using System.Collections.Generic;
using MediatR;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Scales.CreateScale
{
    public sealed class CreateScaleRequest : IRequest
    {
        public string? Name { set; get; }
        public decimal? Ratio { set; get; }
        public ScaleGaugeRequest? Gauge { set; get; }
        public string? Description { set; get; }
        public int? Weight { get; set; }
        public List<string> Standards { get; set; } = new List<string>();
    }
}