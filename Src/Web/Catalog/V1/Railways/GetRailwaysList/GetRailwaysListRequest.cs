using MediatR;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList
{
    public class GetRailwaysListRequest : IRequest
    {
        public GetRailwaysListRequest(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
