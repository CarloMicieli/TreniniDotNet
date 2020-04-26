using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? Owner { get; set; }
    }
}
