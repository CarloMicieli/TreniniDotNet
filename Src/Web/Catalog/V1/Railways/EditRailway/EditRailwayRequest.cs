using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Railways.EditRailway
{
    public sealed class EditRailwayRequest : IRequest
    {
        [JsonIgnore]
        public Slug? Slug { get; set; }

        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public PeriodOfActivityRequest? PeriodOfActivity { set; get; }
        public TotalRailwayLengthRequest? TotalLength { get; set; }
        public RailwayGaugeRequest? Gauge { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Headquarters { get; set; }
    }
}

