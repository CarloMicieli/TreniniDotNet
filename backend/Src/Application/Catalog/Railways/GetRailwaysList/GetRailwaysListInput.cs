using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListInput : IUseCaseInput
    {
        public GetRailwaysListInput(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
