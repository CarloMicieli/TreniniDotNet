using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public sealed class GetScalesListInput : IUseCaseInput
    {
        public GetScalesListInput(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
