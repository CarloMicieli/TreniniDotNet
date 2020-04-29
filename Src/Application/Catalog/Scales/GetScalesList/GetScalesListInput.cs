using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public class GetScalesListInput : IUseCaseInput
    {
        public GetScalesListInput(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
