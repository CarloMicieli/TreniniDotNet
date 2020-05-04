using System;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemRequest : IRequest
    {
        [JsonIgnore]
        public Slug? Slug { set; get; }

        [JsonIgnore]
        public RollingStockId? RollingStockId { set; get; }
    }
}
