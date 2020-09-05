using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Responses;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses
{
    internal class RailwaysListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<RailwayResponse> Results { set; get; }
    }
}