using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Scales.Responses
{
    internal class ScalesListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<ScaleResponse> Results { set; get; }
    }
}