using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleRequest : IRequest
    {
        public string? Name { set; get; }
        public decimal? Ratio { set; get; }
        public decimal? Gauge { set; get; }
        public string? TrackGauge { set; get; }
        public string? Notes { set; get; }
    }
}