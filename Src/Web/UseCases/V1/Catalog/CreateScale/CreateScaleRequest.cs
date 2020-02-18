using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScaleRequest : IRequest
    {
        public string? Name { set; get; }
        public decimal? Ratio { set; get; }
        public decimal? Gauge { set; get; }
    }
}