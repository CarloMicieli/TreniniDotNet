using System;
using MediatR;
using Newtonsoft.Json;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? Owner { get; set; }
    }
}
