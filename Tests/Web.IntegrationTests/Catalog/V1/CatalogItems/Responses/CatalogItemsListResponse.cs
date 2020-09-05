using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses
{
    internal class CatalogItemsListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<CatalogItemResponse> Results { get; set; }
    }
}
