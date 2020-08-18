using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.Scales.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.Scales.EditScale
{
    public sealed class EditScaleRequest : IRequest
    {
        [JsonIgnore]
        public Slug? Slug { get; set; }

        public string? Name { get; set; }

        public decimal? Ratio { get; set; }

        public ScaleGaugeRequest? Gauge { get; set; }

        public string? Description { get; set; }

        public List<string> Standards { get; set; } = new List<string>();

        public int? Weight { get; set; }
    }
}
