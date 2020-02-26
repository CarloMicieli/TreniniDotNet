using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScalesList
{
    public class GetScalesListInput : IUseCaseInput
    {
        public GetScalesListInput(Page? page)
        {
            Page = page;
        }

        public Page? Page { get; }
    }
}
