using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditRailway
{
    public sealed class EditRailwayRequest : IRequest
    {
        [JsonIgnore]
        public Slug Slug { get; set; }

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

