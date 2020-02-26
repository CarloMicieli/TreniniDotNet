using MediatR;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    public class GetRailwaysListRequest : IRequest
    {
        public GetRailwaysListRequest(Page? page)
        {
            Page = page;
        }

        public Page? Page { get; }
    }
}
