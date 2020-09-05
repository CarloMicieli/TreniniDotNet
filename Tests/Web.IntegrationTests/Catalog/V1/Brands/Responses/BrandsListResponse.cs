using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Brands.Responses
{
    internal class BrandsListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<BrandResponse> Results { get; set; }
    }
}