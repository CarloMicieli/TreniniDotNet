using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
