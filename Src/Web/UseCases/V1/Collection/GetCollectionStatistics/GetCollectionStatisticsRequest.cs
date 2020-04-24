using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsRequest : IRequest
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
    }
}
