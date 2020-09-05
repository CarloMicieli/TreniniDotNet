using MediatR;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemRequest : IRequest
    {
        public Slug? Slug { get; set; }

        public RollingStockRequest? RollingStock { get; set; }
    }
}
