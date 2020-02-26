using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList
{
    public sealed class GetRailwaysListInput : IUseCaseInput
    {
        public GetRailwaysListInput(Page? page)
        {
            Page = page;
        }

        public Page? Page { get; }
    }
}
