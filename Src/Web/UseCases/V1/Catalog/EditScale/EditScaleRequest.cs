using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditScale
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
