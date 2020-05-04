using MediatR;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.GetLatestCatalogItems
{
    public sealed class GetLatestCatalogItemsRequest : IRequest
    {
        public Page? Page { set; get; }
    }
}
