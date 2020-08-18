using MediatR;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway
{
    public sealed class CreateRailwayRequest : IRequest
    {
        public string Name { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public PeriodOfActivityRequest? PeriodOfActivity { set; get; }
        public TotalRailwayLengthRequest? TotalLength { get; set; }
        public RailwayGaugeRequest? Gauge { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Headquarters { get; set; }
    }


}
