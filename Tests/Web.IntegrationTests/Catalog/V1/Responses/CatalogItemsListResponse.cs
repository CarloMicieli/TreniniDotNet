using System.Collections.Generic;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Responses
{
    internal class CatalogItemsListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<CatalogItemResponse> Results { get; set; }
    }
}
