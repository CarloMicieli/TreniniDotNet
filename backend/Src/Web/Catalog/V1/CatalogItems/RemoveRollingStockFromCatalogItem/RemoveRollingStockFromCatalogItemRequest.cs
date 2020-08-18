using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

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
